using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BaseDeviceClick : MonoBehaviour
{

    public float radius = 3.5f;

    private void OnMouseDown()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        if (Vector3.Distance(player.position, transform.position) < radius)
        {
            Vector3 direction = transform.position - player.position;
            if (Vector3.Dot(player.forward, direction) > .5f)
            {
                /*
                 * If player looks on device --> Operate
                 */
                Operate();
            }
        }
    }

    /**
     * We can override virtual methods after inheriting 
     */
    public virtual void Operate()
    {
        
    }
}
