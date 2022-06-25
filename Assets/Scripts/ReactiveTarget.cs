using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /**
     * Обьект реагирует на попадание в него
     */
    public void ReactToHit()
    {
        WanderingAI wanderingAI = GetComponent<WanderingAI>();
        
        /*
         * проверяем присоединен ли к персонажу сценарий WanderingAI
         * он может и отсутствовать
         */
        if (wanderingAI != null)
        {
            wanderingAI.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    /**
     * Опрокидываем врага
     * ждем 1,5 секунды
     * убираем врага
     */
    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
        
        // todo can add decrease enemies
        
    }
}