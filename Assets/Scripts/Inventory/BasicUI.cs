using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    private void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int width = 100;
        int height = 30;
        int buffer = 10;

        List<string> itemList = Managers.Inventory.GetItemList();
        if (itemList.Count == 0)
        {
            /*
             * Show message about empty inventory 
             */
            GUI.Box(new Rect(posX, posY, width, height), "No items");
        }

        foreach (string item in itemList)
        {
            int count = Managers.Inventory.GetItemCount(item);
            /*
             * Load resources from Asset/Resources folder
             */
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item);
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image));

            /*
             * Move to the right on each iteration
             */
            posX += width + buffer;
        }

        string equipped = Managers.Inventory.EquippedItem;
        if (equipped != null)
        {
            posX = Screen.width - (width + buffer);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + equipped);
            GUI.Box(
                new Rect(posX, posY, width, height),
                new GUIContent("Equipped", image));
        }

        posX = 10;
        posY += height + buffer;

        /*
         * Iterate over items to create buttons
         */
        foreach (string item in itemList)
        {
            /*
             * On click we go inside if block
             */
            if (GUI.Button(new Rect(posX, posY, width,height), "Equip: " + item))
            {
                /*
                 * If button was clicked we call this code
                 */
                Managers.Inventory.EquipItem(item);
            }

            if ("health" == item)
            {
                if (GUI.Button(new Rect(posX, posY + height + buffer, width, height), "Use health"))
                {
                    Managers.Inventory.ConsumeItem("health");
                    Managers.Player.ChangeHealth(25);
                }
            }

            posX += width + buffer;
        }
    }
}