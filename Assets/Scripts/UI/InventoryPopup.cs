using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private Image[] itemIcons;
    [SerializeField] private TextMeshProUGUI[] itemLabels;

    [SerializeField] private TextMeshProUGUI curItemLabel;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button useButton;

    private string _curItem;

    public void Refresh()
    {
        List<string> itemList = Managers.Inventory.GetItemList();
        int length = itemIcons.Length;
        for (int i = 0; i < length; i++)
        {
            /*
             * Check inventory list
             */
            if (i < itemList.Count)
            {
                itemIcons[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);

                string item = itemList[i];
                Sprite sprite = Resources.Load<Sprite>("icons/" + item);
                itemIcons[i].sprite = sprite;
                /*
                 * Change to native size
                 */
                itemIcons[i].SetNativeSize();

                int count = Managers.Inventory.GetItemCount(item);
                string message = "x" + count;
                if (item == Managers.Inventory.EquippedItem)
                {
                    message = "Equipped\n" + message;
                }

                itemLabels[i].text = message;

                /*
                 * Create interactive objets from icons
                 */
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((data =>
                        /*
                         * With this lambda can activate each object in specific way
                         */
                        OnItem(item)
                    ));
                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            }
            else
            {
                /*
                 * Hide icons and text if out of inventory items
                 */
                itemIcons[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }
        }

        if (!itemList.Contains(_curItem))
        {
            _curItem = null;
        }

        /*
         * Hide buttons if we do not have chosen items
         */
        if (_curItem == null)
        {
            curItemLabel.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        }
        else
        {
            /*
             * Show chosen item
             */
            curItemLabel.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            if (_curItem == "health")
            {
                /*
                 * USE button only for health
                 */
                useButton.gameObject.SetActive(true);
            }
            else
            {
                useButton.gameObject.SetActive(false);
            }

            curItemLabel.text = _curItem + ":";
        }
    }

    public void OnItem(string item)
    {
        _curItem = item;
        /*
         * Refresh inventory after click on item
         */
        Refresh();
    }

    public void OnEquip()
    {
        Managers.Inventory.EquipItem(_curItem);
        Refresh();
    }

    public void OnUse()
    {
        Managers.Inventory.ConsumeItem(_curItem);
        if (_curItem == "health")
        {
            Managers.Player.ChangeHealth(25);
        }
        Refresh();
    }
}