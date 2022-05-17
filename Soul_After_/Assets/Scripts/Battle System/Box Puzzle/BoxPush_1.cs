using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPush_1 : MonoBehaviour
{
    public GameObject Player;
    public PushBoxPuzzleManager_1 puzzleManager;
    public AudioSource IceTileSFX;
    public AudioSource NormSFX;

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
    private ContactPoint2D[] con2Ds = new ContactPoint2D[6];

    void Start()
    {
        targetPos = transform.position;
        DestCalcNode = transform.Find("DestCalcNode").gameObject;
    }

    private void FixedUpdate()
    {
        if (boxTouched && !pushing && Input.GetButtonDown("Jump"))
        {
            teled = false;
            iceTouched = false;
            if (touchedPoint.y == 1)
            {
                PushedPos = 1;
                DestCalcUp();
            }
            else if (touchedPoint.x == 1)
            {
                PushedPos = 2;
                DestCalcRight();
            }
            else if (touchedPoint.x == -1)
            {
                PushedPos = 3;
                DestCalcLeft();
            }
            else
            {
                PushedPos = 4;
                DestCalcDown();
            }
        }

        if (iceTouched)
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.03f);
        else if (!puzzleManager.isAvailable)
            transform.position = Vector3.Lerp(transform.position, targetPos, 1f);
        else
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.3f);

        if (Vector3.Distance(transform.position, targetPos) <= .01f && pushing)
        {
            transform.position = targetPos;
            pushing = false;
            teled = false;
            iceTouched=false;
            if (puzzleManager.isAvailable)
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
            if (first && hit.collider != null && (hit.transform.gameObject.GetComponent<WallTile>() || hit.transform.gameObject.GetComponent<BoxPush_1>()))
            {
                return;
            }
            first = false;

            if (hit.collider != null && (hit.transform.gameObject.GetComponent<PortalActive>() || hit.transform.gameObject.GetComponent<IceTile>()))
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
            if (first && hit.collider != null && (hit.transform.gameObject.GetComponent<WallTile>() || hit.transform.gameObject.GetComponent<BoxPush_1>()))
            {
                return;
            }
            first = false;

            if (hit.collider != null && (hit.transform.gameObject.GetComponent<PortalActive>() || hit.transform.gameObject.GetComponent<IceTile>()))
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
            if (first && hit.collider != null && (hit.transform.gameObject.GetComponent<WallTile>() || hit.transform.gameObject.GetComponent<BoxPush_1>()))
            {
                return;
            }
            first = false;

            if (hit.collider != null && (hit.transform.gameObject.GetComponent<PortalActive>() || hit.transform.gameObject.GetComponent<IceTile>()))
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
            if (first && hit.collider != null && (hit.transform.gameObject.GetComponent<WallTile>() || hit.transform.gameObject.GetComponent<BoxPush_1>()))
            {
                return;
            }
            first = false;

            if (hit.collider != null && (hit.transform.gameObject.GetComponent<PortalActive>() || hit.transform.gameObject.GetComponent<IceTile>()))
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
            else
            {
                PushToDest(DestCalcNode.transform.position);
                return;
            }
        }
    }

    public void PushToDest(Vector3 Dest, bool isSFX = true)
    {
        if (isSFX)
        {
            if (iceTouched && IceTileSFX)
                IceTileSFX.Play();
            else
                NormSFX.Play();
        }
        
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
            int boxCount = 0;
            con2Ds = new ContactPoint2D[6];
            Player.GetComponent<Rigidbody2D>().GetContacts(con2Ds);
            foreach(ContactPoint2D con in con2Ds)
            {
                if(con.collider != null && con.collider.CompareTag("PushBox"))
                {
                    boxCount++;
                }
            }
            if(boxCount <= 2)
            {
                boxTouched = true;
                touchedPoint = collision.GetContact(0).normal;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        boxTouched = false;
    }
}
