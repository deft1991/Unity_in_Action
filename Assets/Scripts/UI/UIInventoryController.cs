using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI healthLabel;
    [SerializeField] private InventoryPopup popup;
    [SerializeField] private TextMeshProUGUI levelEnging;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }

    private void Start()
    {
        /*
         * Call it manually o start
         */
        OnHealthUpdated();
        
        levelEnging.gameObject.SetActive(false);
        
        /*
         * Initialize inventory popup as hide window
         */
        popup.gameObject.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
        }
    }

    private void OnHealthUpdated()
    {
        string message = "Health: " + Managers.Player.health + "/" + Managers.Player.maxHealth;
        healthLabel.text = message;
    }

    private void OnLevelComplete()
    {
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel()
    {
        levelEnging.gameObject.SetActive(true);
        levelEnging.text = "Level Complete!";

        /*
         * Wait for two seconds and go to the next level
         */
        yield return new WaitForSeconds(2);
        
        Managers.Mission.GoToNext();
    }

    private void OnLevelFailed()
    {
        StartCoroutine(FailLevel());
    }

    private IEnumerator FailLevel()
    {
        /*
         * Use the same text field as in success level complete
         */
        levelEnging.gameObject.SetActive(true);
        levelEnging.text = "Level Failed";

        yield return new WaitForSeconds(2);
        
        Managers.Player.Respawn();
        /*
         * After 2 second reload current level again
         */
        Managers.Mission.RestartCurrentLevel();
    }

    public void SaveGame()
    {
        Managers.Data.SaveGameState();
    }

    public void LoadGame()
    {
        Managers.Data.LoadGameState();
    }
    
    private void OnGameComplete()
    {
        levelEnging.gameObject.SetActive(true);
        levelEnging.text = "You Finished the Game!";
    }
}
