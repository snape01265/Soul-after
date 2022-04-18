using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class MarketFunction : MonoBehaviour
{
    private Player player;
    public string[] Items;
    //Panel colors
    public Image[] IMAGE;
    private Color PanelColor = Color.white;
    public void ItemTransaction()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        int ItemID = player.ItemID;
        int i = 0;
        string ItemNo = Items[ItemID];
        foreach (string s in Items)
        {
            //buying an item is triggering installed item to reset
            //buying a new item must not reset the installed item
            if (DialogueLua.GetItemField(s, "State").asBool == true && DialogueLua.GetItemField(s, "Installed").asBool == true)
            {
                DialogueLua.SetItemField(s, "Installed", false);
                IMAGE[i].color = Color.yellow;
                PanelColor = IMAGE[i].color;
            }
            i++;
        }
        if (DialogueLua.GetItemField(ItemNo, "State").asBool == false && DialogueLua.GetItemField(ItemNo, "Installed").asBool == false)
        {
            DialogueLua.SetItemField(ItemNo, "State", true);
            IMAGE[ItemID].color = Color.yellow;
            PanelColor = IMAGE[ItemID].color;
        }
        else if (DialogueLua.GetItemField(ItemNo, "State").asBool == true && DialogueLua.GetItemField(ItemNo, "Installed").asBool == false)
        {
            DialogueLua.SetItemField(ItemNo, "Installed", true);
            IMAGE[ItemID].color = Color.grey;
            PanelColor = IMAGE[ItemID].color;
        }

        if (DialogueLua.GetItemField(ItemNo, "Bought").asBool == false)
        {
            DialogueLua.SetItemField(ItemNo, "Bought", true);
            IMAGE[ItemID].color = Color.grey;
            PanelColor = IMAGE[ItemID].color;
        }
    }
}
