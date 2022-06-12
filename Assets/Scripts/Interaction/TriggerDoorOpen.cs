using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorOpen : MonoBehaviour
{
    /*
     * Position shift relative to the open door 
     */
    [SerializeField] private Vector3 dPos;
    
    private bool _open;

    public void Activate()
    {
        /*
         * Open door only if closed
         */
        if (!_open)
        {
            Vector3 pos = transform.position + dPos;
            transform.position = pos;
            _open = true;
        }
    }

    public void Deactivate()
    {
        /*
         * Close door only if open
         */
        if (_open)
        {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
            _open = false;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + dPos);
    }
}
