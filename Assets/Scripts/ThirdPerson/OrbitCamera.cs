using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{

    /*
     * object to link camera
     */
    [SerializeField] private Transform target;

    public float rotSpeed = 1.5f;
    private float _rotY;
    private Vector3 _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        /*
         * Start diff between object and camera
         */
        _offset = target.position - transform.position;
        
    }

    private void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");

        if (horInput != 0)
        {
            /*
             * Slow camera turn
             */
            _rotY += horInput * rotSpeed;
        }
        else
        {
            /*
             * Fast camera turn for mouse
             */
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        }
        
        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        
        /*
         * Support base offset
         */
        transform.position = target.position - (rotation * _offset);
        
        /*
         * Always look on target 
         */
        transform.LookAt(target);
    }
}
