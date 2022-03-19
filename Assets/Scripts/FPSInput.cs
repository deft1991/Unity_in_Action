using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0F;
    public float gravity = -9.8F;
    public float playerJumpHeight = 2;

    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
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