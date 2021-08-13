using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCActivation : MonoBehaviour
{
    public BoolValue activate;
    public GameObject NPC;
    void Start()
    {
        ActivateObject();
    }
    public void ActivateObject()
    {
        if (activate.initialValue && NPC != null)
        {
            NPC.SetActive(true);
        }
        else if (!activate.initialValue)
        {
            NPC.SetActive(false);
        }
    }
    public void ObjectEnable()
    {
        activate.initialValue = true;
    }
    public void ObjectDisable()
    {
        activate.initialValue = false;
    }
    public void ObjectEnableOnScene()
    {
        NPC.SetActive(true); 
    }    
}
