using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActive : MonoBehaviour
{
    public Transform PortalBuddy;
    private PushBox pushBox;

    private void Start()
    {
        pushBox = GameObject.Find("PushBox").GetComponent<PushBox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pushBox.teled && collision.gameObject.CompareTag("PushBox"))
        {
            Invoke("Tele", .5f);
        }
    }

    private void Tele()
    {
        pushBox.teled = true;
        pushBox.targetPos = PortalBuddy.transform.position;
        pushBox.transform.position = PortalBuddy.position;
        int pushPos = 5 - pushBox.PushedPos;
        switch (pushPos)
        {
            case 1:
                pushBox.DestCalcUp();
                break;
            case 2:
                pushBox.DestCalcRight();
                break;
            case 3:
                pushBox.DestCalcLeft();
                break;
            case 4:
                pushBox.DestCalcDown();
                break;
        }
    }
}
