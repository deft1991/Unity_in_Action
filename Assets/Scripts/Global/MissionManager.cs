using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    
    public int CurLevel { get; private set; }
    public int MaxLevel { get; private set; }

    private NetworkService _networkService;
    
    public void Startup(NetworkService networkService)
    {
        Debug.Log("Mission manager starting...");

        _networkService = networkService;

        CurLevel = 0;
        MaxLevel = 1;

        Status = ManagerStatus.Started;
    }

    public void GoToNext()
    {
        if (CurLevel < MaxLevel)
        {
            CurLevel++;
            string name = "Level" + CurLevel;
            Debug.Log("Loading: " + name);
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.Log("Last level");
        }
    }
}
