using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private Material sky;
    [SerializeField] private Light sun;

    private float _fullIntensity;
    private float _cloudValue = 0f;

    private void Start()
    {
        /*
         * Start intensity == same as full 
         */
        _fullIntensity = sun.intensity;
    }

    private void Update()
    {
        SetOvercast(_cloudValue);
        /*
         * Increase it to have soft slide
         */
        _cloudValue += .0005f;
    }

    private void SetOvercast(float value)
    {
        /*
         * Correcting value of material Blend and light intensity
         *
         * Increase blend, reduce light
         * Reduce blend, increase light
         */
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}