using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyDeviceTrigger : MonoBehaviour
{
    /*
     * Trigger objects for this script
     */
    [SerializeField] private GameObject[] targets;

    public bool requireKey;
    public string keyName;
    public bool isOpenForever;

    private void OnCollisionEnter(Collision collision)
    {
        if (requireKey && keyName != Managers.Inventory.EquippedItem)
        {
            return;
        }

        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        foreach (GameObject target in targets)
        {
            if (!isOpenForever)
            {
                target.SendMessage("Deactivate");
            }
        }
    }
}