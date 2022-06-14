using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private Dictionary<string, int> _items;

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        _items = new Dictionary<string, int>();

        status = ManagerStatus.Started;
    }

    /**
     * Show current inventory
     */
    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in _items)
        {
            itemDisplay += item.Key + " (" + item.Value + ") ";
        }

        Debug.Log(itemDisplay);
    }

    /**
     * Other scripts cannot change item list
     * but can call this method
     */
    public void AddItem(string itemName)
    {
        /*
         * Check that we already have an item
         */
        if (_items.ContainsKey(itemName))
        {
            _items[itemName] += 1;
        }
        else
        {
            _items[itemName] = 1;
        }

        DisplayItems();
    }

    /**
     * Return all keys in dictionary
     */
    public List<string> GetItemList()
    {
        List<string> list = new List<string>(_items.Keys);
        return list;
    }

    /**
     * Return item count in inventory
     */
    public int GetItemCount(string itemName)
    {
        if (_items.ContainsKey(itemName))
        {
            return _items[itemName];
        }

        return 0;
    }
}