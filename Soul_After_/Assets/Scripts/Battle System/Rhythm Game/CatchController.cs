using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatchController : MonoBehaviour
{
    List<List<int>> lst = new List<List<int>>();
    [HideInInspector]
    public List<bool> hpStates;
    private List<HeartRenderer> heartRenderers;

    public int hp;
    public int maxHP;
    private GameObject[] objects;
    /* public OnDestroyEvent[] OnDestroyEvents;
    public UnityEngine.Events.UnityEvent OnAllObjectsDestroyed;*/

    public GameObject player;
    private Transform myTransform;
    public Vector3 topPos;
    public Vector3 centralPos;
    public Vector3 bottomPos;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTransform.position = centralPos;
        hp = maxHP;
        hpStates = Enumerable.Repeat<bool>(true, maxHP).ToList<bool>();
        heartRenderers = Enumerable.Repeat<HeartRenderer>(null, maxHP).ToList<HeartRenderer>();

        objects = GameObject.FindGameObjectsWithTag("HealthObj");
        foreach (GameObject obj in objects)
        {
            HeartRenderer _hpRend = obj.GetComponent<HeartRenderer>();
            if (_hpRend != null)
            {
                char a = obj.name[obj.name.Length - 1];
                int idx = int.Parse(a.ToString()) - 1;
                heartRenderers.Insert(idx, _hpRend);
            }
        }

        /* for (int i = 0; i < OnDestroyEvents.Length; ++i)
            OnDestroyEvents[i].OnObjectDestroyed += OnObjectDestroyed;
    }

    private void OnObjectDestroyed(GameObject destroyedObject)
    {
        CheckAllObjectsAreDestroyed();
    }
    private void CheckAllObjectsAreDestroyed()
    {
        for (int i = 0; i < OnDestroyEvents.Length; ++i)
        {
            if (OnDestroyEvents[i] != null || OnDestroyEvents[i].gameObject != null)
                return;
        }

        if (OnAllObjectsDestroyed != null)
            OnAllObjectsDestroyed.Invoke();*/
    }

    void Update()
    {
        float y = Input.GetAxisRaw("Vertical");

        /* 가운데 고정 이동방식
        if (y > 0)
            player.transform.position = new Vector3(myTransform.position.x, 0.5f, myTransform.position.z);
        else if (y < 0)
            player.transform.position = new Vector3(myTransform.position.x, -3.5f, myTransform.position.z);
        else 
            player.transform.position = new Vector3(myTransform.position.x, -1.5f, myTransform.position.z);
        */

        var posToMove = DeterminePos(transform.position);
        if (posToMove != null)
            transform.position = (Vector3)posToMove;
    }
    private Vector3? DeterminePos(Vector3 pos)
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (transform.position == topPos)
                return topPos;
            if (transform.position == centralPos)
                return topPos;
            if (transform.position == bottomPos)
                return centralPos;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (transform.position == bottomPos)
                return bottomPos;
            if (transform.position == centralPos)
                return bottomPos;
            if (transform.position == topPos)
                return centralPos;
        }
        return null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Flower")
        {
            Destroy(other.gameObject);
        }
    }
    public void TakeDamage(int dmg)
    {
        int oldhp = hp;
        hp -= dmg;
        RenderHp(oldhp, hp);
    }
    public void GetFlower(int dmg)
    {
        int oldhp = hp;
        hp += dmg;
        RenderHp(oldhp, hp);
    }
    private void RenderHp(int oldHp, int newHp)
    {
        if (oldHp > newHp)
        {
            hpStates[newHp] = false;
            heartRenderers[newHp].HPLoss();
        }
        else if (oldHp < newHp)
        {
            hpStates[newHp] = true;
            heartRenderers[newHp].HPGain();
        }
    }
}
