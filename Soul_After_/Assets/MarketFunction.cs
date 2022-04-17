using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class MarketFunction : MonoBehaviour
{
    private Player player;
    public string[] House;
    //Panel colors
    public Image[] IMAGE;
    private Color PanelColor = Color.white;
    public void ItemTransaction()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        int ItemID = player.ItemID;
        int i = 0;
        string HouseNo = House[ItemID];
        foreach (string s in House)
        {
            if (DialogueLua.GetItemField(s, "State").asBool == true && DialogueLua.GetItemField(s, "Installed").asBool == true)
            {
                DialogueLua.SetItemField(s, "Installed", false);
                IMAGE[i].color = Color.yellow;
                PanelColor = IMAGE[i].color;
            }
            i++;
        }
        if (DialogueLua.GetItemField(HouseNo, "State").asBool == false && DialogueLua.GetItemField(HouseNo, "Installed").asBool == false)
        {
            DialogueLua.SetItemField(HouseNo, "State", true);
            IMAGE[ItemID].color = Color.yellow;
            PanelColor = IMAGE[ItemID].color;
        }
        else if (DialogueLua.GetItemField(HouseNo, "State").asBool == true && DialogueLua.GetItemField(HouseNo, "Installed").asBool == false)
        {
            DialogueLua.SetItemField(HouseNo, "Installed", true);
            IMAGE[ItemID].color = Color.grey;
            PanelColor = IMAGE[ItemID].color;
        }
        
    }
}
