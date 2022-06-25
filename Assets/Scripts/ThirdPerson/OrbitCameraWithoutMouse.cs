using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCameraWithoutMouse : MonoBehaviour
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
        float rotY = Input.GetAxis("Horizontal") * rotSpeed;
        
        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        
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
