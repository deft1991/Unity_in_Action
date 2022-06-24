using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLoadingBillboard : MonoBehaviour
{

    [SerializeField] private string imageURL;
    
    public void Operate()
    {
        Managers.Images.GetWebImage(OnWebImage, imageURL);
    }

    /*
     * Callback function
     *
     * Set downloaded image to material
     */
    private void OnWebImage(Texture2D image)
    {
        GetComponent<Renderer>().material.mainTexture = image;
    }
}
