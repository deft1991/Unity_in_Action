using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    /*
     * Скорость движения
     */
    private float speed = 3.0f;

    private bool _alive;

    /*
     * расстояние с которого начинается реакция на препядствия
     */
    private float obstacleRange = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
               * движение только в случае живого персонажа
               */
        if (_alive)
        {
            /*
             * непрерывно движмся в каждом кадре
             * не смотря на повороты
             */
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        /*
         * Луч находится в том же положении
         * и нацеливается в томже направлении что и персонаж
         */
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        /*
         * бросаем луч с описанной вокруг него окружностью
         */
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            if (hit.distance < obstacleRange)
            {
                /*
                 * поворот с наполовину случайным выбором нового направления
                 * наполовину случайным, потому что значения ограничены минимум и максимумом
                 * 
                 */
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    /**
     * открытый метод позволяющий внешнему коду воздействовать на состояние
     */
    public void SetAlive(bool isAlive)
    {
        _alive = isAlive;
    }
}