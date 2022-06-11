using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;

    private GameObject _fireball;

    /*
     * Скорость движения врага
     */
    private float speed = 3.0f;

    private float fireballDistance = 1.5f;

    private bool _alive;

    /*
     * расстояние с которого начинается реакция на препядствия
     */
    private float obstacleRange = 5.0f;
    
    /*
     * Base enemies speed. Configure by speed slider
     */
    private const float baseSpeed = 3.0f;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }

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
            GameObject hitObject = hit.transform.gameObject;
            /*
             * понимаем что это поподание в игрока
             */
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                ThrowFireball();
            }
            else if (hit.distance < obstacleRange)
            {
                RotateEnemy();
            }
        }
    }

    /**
     * поворот с наполовину случайным выбором нового направления
     * наполовину случайным, потому что значения ограничены минимум и максимумом
     */
    private void RotateEnemy()
    {
        float angle = Random.Range(-110, 110);
        transform.Rotate(0, angle, 0);
    }

    private void ThrowFireball()
    {
        if (_fireball == null)
        {
            /*
             * create fireball from prefab
             */
            _fireball = Instantiate(fireballPrefab);

            /*
             * send fireball in the same direction as enemy right now 
             */
            _fireball.transform.position = transform.TransformPoint(Vector3.forward * fireballDistance);
            _fireball.transform.rotation = transform.rotation;
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