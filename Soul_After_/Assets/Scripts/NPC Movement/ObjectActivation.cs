using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivation : MonoBehaviour
{
    public BoolValue activate;
    public GameObject Object;
    void Start()
    {
        ActivateObject();
    }
    public void ActivateObject()
    {
        if (activate.initialValue && Object != null)
        {
            Object.SetActive(true);
        }
        else if (!activate.initialValue && Object != null)
        {
            Object.SetActive(false);
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
        Object.SetActive(true); 
    }    
}
