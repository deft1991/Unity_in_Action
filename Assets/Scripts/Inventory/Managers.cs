using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Required components
 */
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
public class Managers : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }

    /*
     * List of all IGameManagers
     */
    private List<IGameManager> _startSequence;

    /**
     * Executes before start
     * uses for initialization before all other modules
     */
    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager gameManager in _startSequence)
        {
            gameManager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        /*
         * Do it again and again while initializing all modules
         */
        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager gameManager in _startSequence)
            {
                if (ManagerStatus.Started == gameManager.status)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
            }

            /*
             * Stop on one tick before the next check
             */
            yield return null;
        }
        Debug.Log("All managers started up");
    }
}