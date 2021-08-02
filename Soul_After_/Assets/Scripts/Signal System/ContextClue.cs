using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//¸»Ç³¼±
public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;

    public void Enable()
    {
        contextClue.SetActive(true);
    }

    public void Disable()
    {
        contextClue.SetActive(false);
    }

}
