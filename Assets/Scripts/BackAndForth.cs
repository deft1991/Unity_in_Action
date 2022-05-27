using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackAndForth : MonoBehaviour
{
    public float speed = 3.0f;

    /*
     * Движение между maxZ и minZ
     */
    public float maxZ = 16.0f;

    public float minZ = -16.0f;

    /*
     * В каком направлении движется обьект
     */
    private int direction = 1;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, direction * speed * Time.deltaTime);

        bool bounced = false;
        if (startPosition.z + transform.position.z > maxZ
            || startPosition.z + transform.position.z < minZ)
        {
            bounced = true;
            /*
             * Меняем направление движения
             */
            direction = -direction;
        }

        if (bounced)
        {
            /*
             * Делаем дополнительное движение
             * в этом кадре, если обьект поменял направление
             */
            transform.Translate(0, 0, direction * speed * Time.deltaTime);
        }
    }
}