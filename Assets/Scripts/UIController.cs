using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private SettingsPopup settingsPopup;

    private int _score;

    private void Awake()
    {
        /*
         * Set method (OnEnemyHit) as reaction on ENEMY_HIT event
         */
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnDestroy()
    {
        /*
         * If destroy object, remove this subscribe to avoid errors
         */
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Reset score on start 
         */
        _score = 0;
        scoreLabel.text = _score.ToString();
        
        settingsPopup.Close();
    }



    private void OnEnemyHit()
    {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }


    public void OnOpenSettings()
    {
        Debug.Log("open settings");
        settingsPopup.Open();
    }

    public void OnPointerDown()
    {
        Debug.Log("pointer down");
    }
    
    
}
