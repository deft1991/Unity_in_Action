using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    
    public ManagerStatus status { get; private set; }

    private NetworkService _networkService;
    public void Startup(NetworkService networkService)
    {
        Debug.Log("Weather manager starting...");

        _networkService = networkService;

        status = ManagerStatus.Started;
    }
}
