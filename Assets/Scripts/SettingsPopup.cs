using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{

    [SerializeField] private Slider speedSlider;
    [SerializeField] private TMP_InputField nameInput;

    private void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("speed");
        nameInput.text = PlayerPrefs.GetString("name");
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    
    /**
     * Handle input events
     */
    public void OnSubmitName(string name)
    {
        Debug.Log("change name: " + name);
        PlayerPrefs.SetString("name", name);
    }

    /**
     * Handle change speed slider value
     */
    public void OnSpeedValue(float speed)
    {
        Debug.Log("change speed: " + speed);
        PlayerPrefs.SetFloat("speed", speed);
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }
}
