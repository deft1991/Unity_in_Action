using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 9.0F;
    public float sensitivityVert = 9.0F;

    public float minimumVert = -45.0F;
    public float maximumVert = 45.0F;

    private float _rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        DisableRotation();
    }

    /*
     * Disable RigidBody rotation
     */
    private void DisableRotation()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (axes)
        {
            case RotationAxes.MouseX:
            {
                HorizontalRotation();
                break;
            }
            case RotationAxes.MouseY:
            {
                VerticalRotation();
                break;
            }
            case RotationAxes.MouseXAndY:
            {
                // HorizontalRotation();
                // VerticalRotation();
                CombineRotation();
                break;
            }
        }
    }

    /**
     * Combine rotation
     */
    private void CombineRotation()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

        float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        float rotationY = transform.localEulerAngles.y + delta;
        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }

    /**
      * Horizontal rotation
      */
    private void HorizontalRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
    }

    /**
     * Vertical rotation
     */
    private void VerticalRotation()
    {
        var cameraTransform = Camera.main.transform;
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
        float rotationY = cameraTransform.localEulerAngles.y;
        Camera.main.transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }
}