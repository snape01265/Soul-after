using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatchController : MonoBehaviour
{
    [HideInInspector]
    public List<bool> hpStates;
    private List<HeartRenderer> heartRenderers;

    public int hp;
    public int maxHP;
    private GameObject[] objects;

    void Start()
    {
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
    }
    public void TakeDamage(int dmg)
    {
        int oldhp = hp;
        hp -= dmg;
        RenderHp(oldhp, hp);
    }
    public void Heal(int dmg)
    {

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
