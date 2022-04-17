using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    private Image IMAGE;
    private Color PanelColor = Color.white;
    private void Start()
    {
        IMAGE = GetComponent<Image>();
    }
    public void ChangeToYellow()
    {
        IMAGE.color = Color.yellow;
        PanelColor = IMAGE.color;
    }

    public void ChangeToGrey()
    {
        IMAGE.color = Color.grey;
        PanelColor = IMAGE.color;
    }
}
