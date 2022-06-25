using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 1.0f;
    public float moveSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public float pushForce = 3.0f;

    public float deceleration = 25.0f;
    public float targetBuffer = 1.5f;
    private float _curSpeed = 0f;
    private Vector3? _targetPosition = null;

    private CharacterController _characterController;
    private float _vertSpeed;

    /*
     * Use it ta save data between functions
     */
    private ControllerColliderHit _contact;
    private Animator _animator;
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        /*
         * initialize start vert speed as mis speed fall
         * need it for movement
         */
        _vertSpeed = minFall;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(ray, out mouseHit))
            {
                GameObject hitObject = mouseHit.transform.gameObject;
                if (hitObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    _targetPosition = mouseHit.point;
                    _curSpeed = moveSpeed;
                }
            }
        }

        if (_targetPosition != null)
        {
            if (_curSpeed > moveSpeed * .5f)
            {
                // lock rotation when stopping
                Vector3 adjustedPos =
                    new Vector3(_targetPosition.Value.x, transform.position.y, _targetPosition.Value.z);
                Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            }

            movement = _curSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);

            if (Vector3.Distance(_targetPosition.Value, transform.position) < targetBuffer)
            {
                _curSpeed -= deceleration * Time.deltaTime;
                if (_curSpeed <= 0)
                {
                    _targetPosition = null;
                }
            }
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        // raycast down to address steep slopes and dropoff edge
        bool hitGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_characterController.height + _characterController.radius) / 1.9f;
            hitGround = hit.distance <= check; // to be sure check slightly beyond bottom of capsule
        }

        // y movement: possibly jump impulse up, always accel down
        // could _charController.isGrounded instead, but then cannot workaround dropoff edge
        if (hitGround)
        {
            // commented out lines remove jump control
            //if (Input.GetButtonDown("Jump")) {
            //	__vertSpeed = jumpSpeed;
            //} else {
            _vertSpeed = minFall;
            _animator.SetBool("Jumping", false);
            //}
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }

            if (_contact != null)
            {
                // not right at level start
                _animator.SetBool("Jumping", true);
            }

            // workaround for standing on dropoff edge
            if (_characterController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }

        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _characterController.Move(movement);
    }

    /*
     * Move in horizontal
     */
    private Vector3 HorizontalMovement(Vector3 movement)
    {
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        /*
         * Horizontal moving
         * 
         * Check only key movement
         */
        if (horInput != 0 || vertInput != 0)
        {
            /*
             * Add speed
             */
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;

            /*
             * add borders to diagonal movement
             */
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            /*
             * Save base orientation
             */
            Quaternion tmp = target.rotation;

            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        return movement;
    }

    /**
     * Save information about hit
     */
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        /*
         * Check that hit object has rigitbody
         */
        if (body != null && !body.isKinematic)
        {
            /*
             * Set velocity to physic body
             */
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}