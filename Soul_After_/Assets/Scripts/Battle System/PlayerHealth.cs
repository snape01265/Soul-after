using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class PlayerHealth : MonoBehaviour
{
    List<List<int>> lst = new List<List<int>>();
    [HideInInspector]
    public List<bool> hpStates;
    private List<HeartRenderer> heartRenderers;

    public FloatValue CurHP;
    public int maxHP;
    public bool levelClear;
    public AudioSource hitSound;

    private CameraShake shake;
    private IEnumerator currentIFrame;
    private GameObject[] objects;

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

    private bool ShieldBroken = false;

    void Start()
    {
        hpStates = Enumerable.Repeat<bool>(true, maxHP).ToList<bool>();
        heartRenderers = Enumerable.Repeat<HeartRenderer>(null, maxHP).ToList<HeartRenderer>();

        if (GameObject.FindGameObjectWithTag("ScreenShake"))
            shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();

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

        if ((int) CurHP.initialValue != maxHP)
        {
            RenderHp(maxHP, (int)CurHP.initialValue);
        }
    }

    public void TakeDamage(int dmg)
    {
        int oldhp = (int)CurHP.initialValue;
        if (ShieldEnabled && !ShieldBroken)
        {
            ShieldBroken = true;
            StartCoroutine(FlipAfter(CD));
        }
        else
        {
            CurHP.initialValue -= dmg;
            RenderHp(oldhp, (int)CurHP.initialValue);
        }

        if ((int) CurHP.initialValue <= 0)
        {
            GameObject.Find("GameOverCutscene").GetComponent<PlayableDirector>().Play();
            mySprite.color = regularColor;
        }
        else if (currentIFrame == null)
        {
            StartCoroutine(IFrame());
            currentIFrame = IFrame();
        }
    }

    public void RestoreHealth()
    {
        RenderHp((int)CurHP.initialValue, maxHP);
        CurHP.initialValue = maxHP;
    }

    public void AddHeart()
    {
        string lastHeartName = "Heart_" + maxHP.ToString();
        GameObject lastHeart = GameObject.Find(lastHeartName);

        maxHP += 1;
        GameObject newHeart = Instantiate(lastHeart);
        newHeart.name = "Heart_" + maxHP.ToString();
        newHeart.tag = "HealthObj";
        
        newHeart.transform.SetParent(GameObject.Find("Health").transform);
        newHeart.GetComponent<RectTransform>().localScale = new Vector3(.75f, .75f, .75f);
        newHeart.GetComponent<RectTransform>().position = lastHeart.GetComponent<RectTransform>().position + new Vector3(25f, 0, 0);
    }

    public void RenderHp(int oldHp, int newHp)
    {
        if (oldHp > newHp)
        {
            for (int i = oldHp - 1; i >= newHp; i--)
            {
                hpStates[i] = false;
                heartRenderers[i].HPLoss();
            }
        }
        else if (oldHp < newHp)
        {
            for (int i = oldHp; i < newHp; i++)
            {
                hpStates[i] = true;
                heartRenderers[i].HPGain();
            }
        }
    }

    IEnumerator IFrame()
    {
        int temp = 0;
        triggerCollider.enabled = false;

        if (hitSound)
            hitSound.Play();

        if (shake)
            shake.CamShake();

        if (ShieldBroken)
        {
            yield return new WaitForSeconds(numberOfFlashes * flashDuration * 2);
        } else
        {
            while (temp < numberOfFlashes)
            {
                mySprite.color = flashColor;
                yield return new WaitForSeconds(flashDuration);
                mySprite.color = regularColor;
                yield return new WaitForSeconds(flashDuration);
                temp++;
            }
        }

        currentIFrame = null;
        triggerCollider.enabled = true;
    }
    
    IEnumerator FlipAfter(int Time)
    {
        yield return new WaitForSeconds(Time);
        ShieldBroken = false;
    }
}