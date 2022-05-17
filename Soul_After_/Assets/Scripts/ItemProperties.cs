using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class ItemProperties : MonoBehaviour
{
    public int ID;
    public int Price;

    private Player player;
    private Image image;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();    
    }
    public void ReturnID()
    {
        player.ItemID = ID - 1;
    }
}
