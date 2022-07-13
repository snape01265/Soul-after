using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class PlayerHealth : MonoBehaviour
{
    public FloatValue CurHP;
    public int maxHP;
    public AudioSource hitSound;

    [Header("Invulnerability Frame")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    [Header("Shield")]
    public bool ShieldEnabled = false;
    public int CD;
    public Animator ShieldAnim;

    private List<HeartRenderer> heartRenderers;
    private IEnumerator currentIFrame;
    [NonSerialized]
    public bool PainState = false;
    private bool ShieldBroken = false;
    private bool ShieldCD = false;
    private readonly float RECHARGEANIMTIME = 1f;

    void Start()
    {
        heartRenderers = Enumerable.Repeat<HeartRenderer>(null, maxHP).ToList();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("HealthObj");
        foreach (GameObject obj in objects)
        {
            HeartRenderer _hpRend = obj.GetComponent<HeartRenderer>();
            if (_hpRend != null)
            {
                char a = obj.name[obj.name.Length - 1];
                int idx = int.Parse(a.ToString()) - 1;
                heartRenderers[idx] =  _hpRend;
            }
        }

        if ((int) CurHP.initialValue != maxHP)
        {
            RenderHp(maxHP, (int)CurHP.initialValue);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (!PainState)
        {
            PainState = true;
            int oldhp = (int)CurHP.initialValue;
            if (ShieldEnabled && !ShieldBroken && !ShieldCD)
            {
                ShieldBroken = true;
                ShieldCD = true;
                StartCoroutine(FlipAfter(CD));
            }
            else
            {
                CurHP.initialValue -= dmg;
                RenderHp(oldhp, (int)CurHP.initialValue);
            }

            if ((int)CurHP.initialValue <= 0)
            {
                StopAllCoroutines();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CancelControl();
                PlayableDirector deathTimeline = GameObject.Find("Gameover").GetComponent<PlayableDirector>();
                deathTimeline.Play();
                mySprite.color = regularColor;
            }
            else if (currentIFrame == null)
            {
                StartCoroutine(IFrame());
                currentIFrame = IFrame();
            }
        }
    }

    public void RestoreHealth()
    {
        RenderHp((int)CurHP.initialValue, maxHP);
        CurHP.initialValue = maxHP;
    }

    public void RenderHp(int oldHp, int newHp)
    {
        if (oldHp > newHp)
        {
            for (int i = oldHp - 1; i >= newHp; i--)
            {
                heartRenderers[i].HPLoss();
            }
        }
        else if (oldHp < newHp)
        {
            for (int i = oldHp; i < newHp; i++)
            {
                heartRenderers[i].HPGain();
            }
        }
    }

    IEnumerator IFrame()
    {
        if (hitSound)
            hitSound.Play();

        if (ShieldBroken)
        {
            ShieldBroken = false;
            ShieldAnim.SetBool("Broken", true);
            yield return new WaitForSeconds(numberOfFlashes * flashDuration * 2);
        } else
        {
            for(int i=0; i < numberOfFlashes; i++)
            {
                mySprite.color = flashColor;
                yield return new WaitForSeconds(flashDuration);
                mySprite.color = regularColor;
                yield return new WaitForSeconds(flashDuration);
            }
        }
        currentIFrame = null;
        PainState = false;
    }

    public void PlayerInvulnerable()
    {
        triggerCollider.enabled = false;
    }

    public void PlayerVulnerable()
    {
        triggerCollider.enabled = true;
    }

    IEnumerator FlipAfter(int Time)
    {
        yield return new WaitForSeconds(Time - RECHARGEANIMTIME);
        ShieldAnim.SetBool("Broken", false);
        yield return new WaitForSeconds(RECHARGEANIMTIME);
        ShieldCD = false;
    }
}