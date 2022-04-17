using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemProperties : MonoBehaviour
{
    public int ID;
    public int Price;
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void ReturnID()
    {
        player.ItemID = ID - 1;
    }
}
