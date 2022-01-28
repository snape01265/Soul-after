using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    public Transform playerPos;
    
    [HideInInspector]
    // 1 : UP, 2 : RIGHT, 3 : LEFT, 4 : DOWN 
    public int PushedPos = 0;
    [HideInInspector]
    public Vector3 LastLoc;
    [HideInInspector]
    public bool teled = false;
    [HideInInspector]
    public Vector3 targetPos;

    private GameObject DestCalcNode;
    private bool boxTouched = false;
    private bool pushing = false;
    private Vector2 touchedPoint;
    

    void Start()
    {
        targetPos = transform.position;
        DestCalcNode = transform.Find("DestCalcNode").gameObject;
        LastLoc = transform.position;
    }

    private void Update()
    {
        if (boxTouched && !pushing && Input.GetButtonDown("Jump"))
        {
            pushing = true;
            if (touchedPoint.y == 1)
            {
                PushedPos = 1;
                DestCalcUp();
            } else if (touchedPoint.x == 1)
            {
                PushedPos = 2;
                DestCalcRight();
            } else if (touchedPoint.x == -1)
            {
                PushedPos = 3;
                DestCalcLeft();
            } else
            {
                PushedPos = 4;
                DestCalcDown();
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.05f);
        if (Vector3.Distance(transform.position, targetPos ) <= .03f)
        {
            transform.position = targetPos;
            pushing = false;
        }
    }

    public void DestCalcUp()
    {
        DestCalcNode.transform.position = Vector3Int.RoundToInt(transform.position + Vector3.up);
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(DestCalcNode.transform.position, DestCalcNode.transform.forward);
            if (hit.collider != null && !hit.transform.gameObject.GetComponent<PortalActive>() && !hit.transform.gameObject.GetComponent<WallTile>() && hit.transform.gameObject.GetComponent<IceTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position + Vector3.up);
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position - Vector3.up);
                PushToDest(DestCalcNode.transform.position);
                return;
            }
            else
            {
                PushToDest(DestCalcNode.transform.position);
                return;
            }

        }
    }

    public void DestCalcDown()
    {
        DestCalcNode.transform.position = Vector3Int.RoundToInt(transform.position + Vector3.down);
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(DestCalcNode.transform.position, DestCalcNode.transform.forward);
            if (hit.collider != null && !hit.transform.gameObject.GetComponent<PortalActive>() && !hit.transform.gameObject.GetComponent<WallTile>() && hit.transform.gameObject.GetComponent<IceTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position + Vector3.down);
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position - Vector3.down);
                PushToDest(DestCalcNode.transform.position);
                return;
            }
            else
            {
                PushToDest(DestCalcNode.transform.position);
                return;
            }

        }
    }

    public void DestCalcRight()
    {
        DestCalcNode.transform.position = Vector3Int.RoundToInt(transform.position + Vector3.right);

        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(DestCalcNode.transform.position, DestCalcNode.transform.forward);

            if (hit.collider != null && !hit.transform.gameObject.GetComponent<PortalActive>() && !hit.transform.gameObject.GetComponent<WallTile>() && hit.transform.gameObject.GetComponent<IceTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position + Vector3.right);
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position - Vector3.right);
                PushToDest(DestCalcNode.transform.position);
                return;
            }
            else
            {
                PushToDest(DestCalcNode.transform.position);
                return;
            }
        }
    }

    public void DestCalcLeft()
    {
        DestCalcNode.transform.position = Vector3Int.RoundToInt(transform.position + Vector3.left);

        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(DestCalcNode.transform.position, DestCalcNode.transform.forward);

            if (hit.collider != null && !hit.transform.gameObject.GetComponent<PortalActive>() && !hit.transform.gameObject.GetComponent<WallTile>() && hit.transform.gameObject.GetComponent<IceTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position + Vector3.left);
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position - Vector3.left);
                PushToDest(DestCalcNode.transform.position);
                return;
            }
            else
            {
                PushToDest(DestCalcNode.transform.position);
                return;
            }
        }
    }

    public void PushToDest(Vector3 Dest)
    {
        if (!teled)
        {
            LastLoc = transform.position;
        }
        targetPos = Dest;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            boxTouched = true;
            touchedPoint = collision.GetContact(0).normal;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        boxTouched = false;
    }
}