using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class MarketFunction : MonoBehaviour
{   
    [HideInInspector]
    public string[] Items;
    [HideInInspector]
    public Image[] IMAGE;

    private Color PanelColor = Color.white;
    private Player player;

    private void OnEnable()
    {
        CheckItemStatus();
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
            }
        }
        //Houses
        else if (DialogueLua.GetItemField(ItemNo, "isHouse").asBool == true)
        {
            if (DialogueLua.GetItemField(ItemNo, "State").asBool == false && DialogueLua.GetItemField(ItemNo, "Installed").asBool == false)
            {
                if (player.Token.initialValue > 0 && player.Token.initialValue >= player.Items[ItemID].Price)
                {
                    DialogueLua.SetItemField(ItemNo, "State", true);
                    DialogueLua.SetItemField(ItemNo, "Installed", true);
                    IMAGE[ItemID].color = Color.grey;
                    PanelColor = IMAGE[ItemID].color;
                    player.MakePayments();
                }
                else
                {
                    player.NoMoneyTab.SetActive(true);
                }
            }

            foreach (string s in Items)
            {
                if (DialogueLua.GetItemField(s, "State").asBool == true && DialogueLua.GetItemField(s, "Installed").asBool == true)
                {
                    DialogueLua.SetItemField(s, "Installed", false);
                    IMAGE[i].color = Color.yellow;
                    PanelColor = IMAGE[i].color;
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
                }
                else
                {
                    IMAGE[i].color = Color.grey;
                    PanelColor = IMAGE[i].color;
                }
            }
            else
            {
                if (DialogueLua.GetItemField(s, "State").asBool == false && DialogueLua.GetItemField(s, "Installed").asBool == false)
                {
                    IMAGE[i].color = Color.white;
                    PanelColor = IMAGE[i].color;
                }
                else if (DialogueLua.GetItemField(s, "State").asBool == true && DialogueLua.GetItemField(s, "Installed").asBool == false)
                {
                    IMAGE[i].color = Color.yellow;
                    PanelColor = IMAGE[i].color;
                }
                else if (DialogueLua.GetItemField(s, "State").asBool == true && DialogueLua.GetItemField(s, "Installed").asBool == true)
                {
                    IMAGE[i].color = Color.grey;
                    PanelColor = IMAGE[i].color;
                }
            }
            i++;
        }



    }
}
