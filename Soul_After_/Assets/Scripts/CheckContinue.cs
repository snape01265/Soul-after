using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CheckContinue : MonoBehaviour
{
    void Start()
    {
        for (int n = 0; n < 3; n++)
        {
            try
            {
                if (File.Exists(Application.persistentDataPath +
                string.Format("/Save{0}.dat", n)))
                {
                    GetComponent<Button>().interactable = true;
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.StackTrace);
            }
        }
        
    }
}
