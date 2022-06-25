using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUIController : MonoBehaviour
{

    [SerializeField] private SoundSettingsPopup soundSettingsPopup;
    
    // Start is called before the first frame update
    void Start()
    {
        soundSettingsPopup.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            /*
             * Turn on/off settingsPopup
             */
            bool isShowing = soundSettingsPopup.gameObject.activeSelf;
            soundSettingsPopup.gameObject.SetActive(!isShowing);

            /*
             * Also show cursor with popup window
             */
            if (isShowing)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
