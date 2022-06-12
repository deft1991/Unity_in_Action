using System;
using System.Collections;
using System.Collections.Generic;
// using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0F;
    public float gravity = -9.8F;
    public float playerJumpHeight = 2;

    // private PhotonView _photonView;
    private CharacterController _characterController;

    private float baseSpeed = 6.0f;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        // _photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
         * Move the player object that belongs to the local user (photonView.IsMine)
         *  Each player will need to see the other player’s object,
         * so we’ll create a player object for each player when they join the room.
         * However, we don’t want other users controlling our local character.
         */
        // if (_photonView.IsMine)
        // {
            Move();
        // }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        float deltaY = gravity;
        if (Input.GetKey(KeyCode.Space))
        {
            deltaY = playerJumpHeight;
        }

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        /*
             * Ограничиваем движение по диагонали
             * той же скоростью что и по осям
             */
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = deltaY;
        movement *= Time.deltaTime;

        /*
             * Преобразуем вестор движегия от локальных
             * к глобальным координатам
             */
        movement = transform.TransformDirection(movement);

        /*
             * Заставим этот вектор перемещать компонент
             * CharacterController
             */
        _characterController.Move(movement);
    }
}