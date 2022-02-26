using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public RotationAxes axes = RotationAxes.MouseXAndX;    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (axes)
        {
            case RotationAxes.MouseX:
            {
                /*
                 * Horizontal rotation
                 */
                break;
            }
            case RotationAxes.MouseY:
            {
                /*
                 * Vertical rotation
                 */
                break;
            }
            case RotationAxes.MouseXAndX:
            {
                /*
                 * Combine rotation
                 */
                break;
            }
                
        }
    }
}
