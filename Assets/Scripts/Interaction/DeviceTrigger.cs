using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    /*
     * Trigger objects for this script
     */
    [SerializeField] private GameObject[] targets;


    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
           target.SendMessage("Deactivate");
        }
    }
}