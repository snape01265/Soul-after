using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    public Transform playerPos;
    public PushBoxPuzzleManager puzzleManager;
    public AudioSource iceTileSFX;
    public AudioSource normSFX;

    [HideInInspector]
    // 1 : UP, 2 : RIGHT, 3 : LEFT, 4 : DOWN 
    public int PushedPos = 0;
    [HideInInspector]
    public Vector3 LastLoc;
    [HideInInspector]
    public bool teled = false;
    [HideInInspector]
    public Vector3 targetPos;
    [HideInInspector]
    public bool pushing = false;

    private GameObject DestCalcNode;
    private bool iceTouched;
    private bool boxTouched = false;
    private Vector2 touchedPoint;    

    void Start()
    {
        targetPos = transform.position;
        DestCalcNode = transform.Find("DestCalcNode").gameObject;
    }

    private void Update()
    {
        if (boxTouched && !pushing && Input.GetButtonDown("Jump"))
        {
            teled = false;
            iceTouched = false;
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
        if (iceTouched)
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.03f);
        else if (puzzleManager.isReset)
            transform.position = Vector3.Lerp(transform.position, targetPos, 1f);
        else
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.15f);

        if (Vector3.Distance(transform.position, targetPos) <= .01f && pushing)
        {
            transform.position = targetPos;
            pushing = false;
            teled = false;
            if (!puzzleManager.isReset)
                puzzleManager.TurnCount -= 1;
        }
    }

    public void DestCalcUp()
    {
        DestCalcNode.transform.position = Vector3Int.RoundToInt(transform.position + Vector3.up);
        bool first = true;
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(DestCalcNode.transform.position, DestCalcNode.transform.forward);
            if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>() && first)
            {
                return;
            }
            first = false;

            if (hit.collider != null && !hit.transform.gameObject.GetComponent<PortalActive>() && !hit.transform.gameObject.GetComponent<WallTile>() && hit.transform.gameObject.GetComponent<IceTile>())
            {
                iceTouched = true;
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position + Vector3.up);
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position - Vector3.up);
                PushToDest(DestCalcNode.transform.position);
                return;
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<FinTile>())
            {
                puzzleManager.goalReached = true;
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
        bool first = true;
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(DestCalcNode.transform.position, DestCalcNode.transform.forward);
            if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>() && first)
            {
                return;
            }
            first = false;

            if (hit.collider != null && !hit.transform.gameObject.GetComponent<PortalActive>() && !hit.transform.gameObject.GetComponent<WallTile>() && hit.transform.gameObject.GetComponent<IceTile>())
            {
                iceTouched = true;
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position + Vector3.down);
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position - Vector3.down);
                PushToDest(DestCalcNode.transform.position);
                return;
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<FinTile>())
            {
                puzzleManager.goalReached = true;
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
        bool first = true;
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(DestCalcNode.transform.position, DestCalcNode.transform.forward);
            if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>() && first)
            {
                return;
            }
            first = false;

            if (hit.collider != null && !hit.transform.gameObject.GetComponent<PortalActive>() && !hit.transform.gameObject.GetComponent<WallTile>() && hit.transform.gameObject.GetComponent<IceTile>())
            {
                iceTouched = true;
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position + Vector3.right);
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position - Vector3.right);
                PushToDest(DestCalcNode.transform.position);
                return;
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<FinTile>())
            {
                puzzleManager.goalReached = true;
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
        bool first = true;
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(DestCalcNode.transform.position, DestCalcNode.transform.forward);
            if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>() && first)
            {
                return;
            }
            first = false;

            if (hit.collider != null && !hit.transform.gameObject.GetComponent<PortalActive>() && !hit.transform.gameObject.GetComponent<WallTile>() && hit.transform.gameObject.GetComponent<IceTile>())
            {
                iceTouched = true;
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position + Vector3.left);
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<WallTile>())
            {
                DestCalcNode.transform.position = Vector3Int.RoundToInt(DestCalcNode.transform.position - Vector3.left);
                PushToDest(DestCalcNode.transform.position);
                return;
            }
            else if (hit.collider != null && hit.transform.gameObject.GetComponent<FinTile>())
            {
                puzzleManager.goalReached = true;
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
        if (iceTouched && iceTileSFX)
            iceTileSFX.Play();
        else
            normSFX.Play();

        if (!teled)
        {
            LastLoc = transform.position;
        }
        targetPos = Dest;
        pushing = true;
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
