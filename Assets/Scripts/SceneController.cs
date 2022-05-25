using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    /*
     * префаб врага
     * сериализованная переменная для связи с обьектом шаблоном
     */
    [SerializeField] private GameObject enemyPrefab;

    private GameObject _enemy;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * создаем нового врага только если враги отсутствуют
         */
        if (_enemy == null)
        {
            /*
             * метод копирующий обьект шаблон
             */
            _enemy = Instantiate(enemyPrefab);
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angel = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angel, 0);
        }
    }
}