using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float gravity = -9.8f;
    private CharacterController _characterController; // Переменная для ссылкина компонент CharacterController.

    // Start is called before the first frame update
    void Start()
    {
        _characterController =
            GetComponent<CharacterController>(); // Доступ к другим компонен- там, присоединеннымк этому же объекту.
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement,
            speed); // Ограничим движение по диа- гонали той же скоростью, что и движение параллельно осям.
        movement.y = gravity; // Используем значение переменной gravity вместо нуля.
        movement *= Time.deltaTime;
        movement = transform
            .TransformDirection(movement); // Преобразуем вектор движения от локальных к глобальным координатам.
        _characterController.Move(movement); // Заставим этот вектор перемещать компонент CharacterController.
    }
}