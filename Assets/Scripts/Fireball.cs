using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    [Tooltip("Fireball speed")]
    [SerializeField]
    private float speed = 10f;
    
    [Tooltip("Fireball damage")]
    [SerializeField]
    private int damage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0, speed * Time.deltaTime);
    }

    /**
     * call this function when this object collider with other object
     */
    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("Player HIT!");
            player.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
}
