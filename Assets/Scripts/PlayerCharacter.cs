using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    [Tooltip("Player health")]
    [SerializeField]
    private int _health = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int damage)
    {
        _health -= damage;
        Managers.Player.ChangeHealth(-damage);
    }
}
