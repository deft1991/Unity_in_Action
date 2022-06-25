using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter3rdPerson : MonoBehaviour
{

 [SerializeField] private AudioSource soundSource;
 [SerializeField] private AudioClip hitWallSound;
 [SerializeField] private AudioClip hitEnemySound;
 
    [Tooltip("The scope size")] [SerializeField]
    private int scopeSize = 12;

    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Доступ к другим компонентам
         * присоединенным к этому же обьекту. 
         */
        _camera = gameObject.GetComponentInChildren<Camera>();

        /*
         * Скрываем указатель мыши в центре экрана
         */
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void OnGUI()
    {
        float posX = _camera.pixelWidth / 2 - scopeSize / 4;
        float posY = _camera.pixelHeight / 2 - scopeSize / 2;
        /*
         *  GUI.Label отображает на экране символ
         */
        GUI.Label(new Rect(posX, posY, scopeSize, scopeSize), "*");
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Реакция на нажатие кнопки мыши
         */
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
           
            /*
             * Создание в точке point луча методом ScreenPointToRay
             */
            
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit raycastHit;

            /*
             * Выпущенный луч заполняет информацией переменную, на которую имеется ссылка
             */
            if (Physics.Raycast(ray, out raycastHit))
            {
                Debug.Log("Hit " + raycastHit.point);
                /*
                 * Получаем обьект в который попал луч
                 */
                GameObject hitGameObject = raycastHit.transform.gameObject;

                ReactiveTarget reactiveTarget = hitGameObject.GetComponent<ReactiveTarget>();
                /*
                 * Проверяем наличие у этого компонента ReactiveTarget
                 */
                if (reactiveTarget != null)
                {
                 Debug.Log("Target hit");
                 reactiveTarget.ReactToHit();
                 soundSource.PlayOneShot(hitEnemySound);
                 /*
                  * Broadcast ENEMY_HIT event
                  */
                 Messenger.Broadcast(GameEvent.ENEMY_HIT);
                 
                }
                else
                {
                    /*
                     * Загружаем координаты точки, в которую попал луч.
                     */
                    StartCoroutine(SphereIndicator(raycastHit.point));
                    soundSource.PlayOneShot(hitWallSound);
                }
            }
        }
    }

    /**
     * Сопрограммы используются функциями IEnumerator
     */
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        /*
         * Ключевое слово yield указывает сопрограмме, когда следует остановиться
         */
        yield return new WaitForSeconds(1);

        /*
         * Удаляем этот GameObject и очищаем память
         */
        Destroy((sphere));
    }
}