using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class MarketFunction : MonoBehaviour
{   
    public string[] Items;
    public Image[] IMAGE;

    private Color PanelColor = Color.white;
    private Player player;

    private void OnEnable()
    {
        CheckItemStatus();
        Debug.Log("Checking item status");
    }

    public void ItemTransaction()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        int ItemID = player.ItemID;
        int i = 0;
        string ItemNo = Items[ItemID];

        //Items
        if (DialogueLua.GetItemField(ItemNo, "isHouse").asBool == false)
        {
            if (DialogueLua.GetItemField(ItemNo, "Bought").asBool == false)
            {
                DialogueLua.SetItemField(ItemNo, "Bought", true);
                IMAGE[ItemID].color = Color.grey;
                PanelColor = IMAGE[ItemID].color;
                Debug.Log("It turned grey!");
            }
        }
        //Houses
        else if (DialogueLua.GetItemField(ItemNo, "isHouse").asBool == true)
        {
            if (DialogueLua.GetItemField(ItemNo, "State").asBool == false && DialogueLua.GetItemField(ItemNo, "Installed").asBool == false)
            {
                DialogueLua.SetItemField(ItemNo, "State", true);
                IMAGE[ItemID].color = Color.yellow;
                PanelColor = IMAGE[ItemID].color;
                Debug.Log("It turned yellow!");
                return;
            }

            foreach (string s in Items)
            {
                if (DialogueLua.GetItemField(s, "State").asBool == true && DialogueLua.GetItemField(s, "Installed").asBool == true)
                {
                    DialogueLua.SetItemField(s, "Installed", false);
                    IMAGE[i].color = Color.yellow;
                    PanelColor = IMAGE[i].color;
                    Debug.Log("House dismantled");
                }
                i++;
            }

            if (DialogueLua.GetItemField(ItemNo, "State").asBool == true && DialogueLua.GetItemField(ItemNo, "Installed").asBool == false)
            {
                DialogueLua.SetItemField(ItemNo, "Installed", true);
                IMAGE[ItemID].color = Color.grey;
                PanelColor = IMAGE[ItemID].color;
            }
        }     
    }

    public void CheckItemStatus()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        int ItemID = player.ItemID;
        int i = 0;
        string ItemNo = Items[ItemID];

        foreach (string s in Items)
        {
            if (DialogueLua.GetItemField(s, "isHouse").asBool == false)
            {
                if (DialogueLua.GetItemField(s, "Bought").asBool == false)
                {
                    IMAGE[i].color = Color.white;
                    PanelColor = IMAGE[i].color;
                    Debug.Log("Item stays white!");
                }
                else
                {
                    IMAGE[i].color = Color.grey;
                    PanelColor = IMAGE[i].color;
                    Debug.Log("Item stays grey!");
                }
            }
            else
            {
                if (DialogueLua.GetItemField(s, "State").asBool == false && DialogueLua.GetItemField(s, "Installed").asBool == false)
                {
                    IMAGE[i].color = Color.white;
                    PanelColor = IMAGE[i].color;
                    Debug.Log("House stays white!");
                }
                else if (DialogueLua.GetItemField(s, "State").asBool == true && DialogueLua.GetItemField(s, "Installed").asBool == false)
                {
                    IMAGE[i].color = Color.yellow;
                    PanelColor = IMAGE[i].color;
                    Debug.Log("House stays yellow!");
                }
                else if (DialogueLua.GetItemField(s, "State").asBool == true && DialogueLua.GetItemField(s, "Installed").asBool == true)
                {
                    IMAGE[i].color = Color.grey;
                    PanelColor = IMAGE[i].color;
                    Debug.Log("House stays grey!");
                }
            }
            i++;
        }



    }
}
