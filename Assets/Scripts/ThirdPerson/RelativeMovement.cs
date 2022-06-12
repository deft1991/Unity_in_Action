using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 1.0f;
    public float moveSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public float pushForce = 3.0f;

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
        /*
         * Start from zero
         * and add movement components step by step
         */
        Vector3 movement = Vector3.zero;

        /*
         * Move in horizontal
         */
        movement = HorizontalMovement(movement);

        /*
         * Move in Vertical (Jump/Fall)
         */
        movement = VerticalMovement(movement);

        _characterController.Move(movement);
    }

    /*
 * Move in Vertical (Jump/Fall)
 */
    private Vector3 VerticalMovement(Vector3 movement)
    {
        bool hitGround = false;
        RaycastHit hit;
        /*
         * Check that player falls
         */
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_characterController.height + _characterController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }
        
        _animator.SetFloat(Speed, movement.sqrMagnitude);

        /*
         * Jump jump jump
         *
         * Vertical moving
         *
         * if character touch ground
         */
        if (hitGround)
        {
            /*
             * reaction on Jump click
             */
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
                _animator.SetBool(Jumping, false);
            }
        }
        else
        {
            /*
             * Use gravity while do not have max speed
             */
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }

            /*
             * Do not start clip on scene start event
             */
            if (_contact != null)
            {
                _animator.SetBool(Jumping, true);
            }

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
        return movement;
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