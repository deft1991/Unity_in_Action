using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{

    [SerializeField] private string itemName;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Item collected: " + itemName);
        
        /*
         * Add item to inventory 
         */
        Managers.Inventory.AddItem(itemName);
        
        /*
         * If call this --> destroy script without game object
         * have to destroy game object
         */
        Destroy(this.gameObject);
    }
}
