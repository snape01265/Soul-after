using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector]
    public List<bool> hpStates;
    private List<HeartRenderer> heartRenderers;

    public int hp;
    public int maxHP;
    public bool levelClear;
    public AudioSource hitSound;
    public AudioSource acqSound;
    public RPGTalk rpgTalk;

    private Gameover gameOver;
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

    void Start()
    {
        hp = maxHP;
        hpStates = new List<bool>(4) {true, true, true, true};
        heartRenderers = new List<HeartRenderer>(4) { null, null, null, null };

        if (GameObject.Find("GameOver"))
        {
            gameOver = GameObject.Find("GameOver").GetComponent<Gameover>();
        }
        if (GameObject.FindGameObjectWithTag("ScreenShake"))
        {
            shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        }

        objects = GameObject.FindGameObjectsWithTag("HealthObj");
        Debug.Log("obj len : " + objects.Length);
        foreach (GameObject obj in objects)
        {
            HeartRenderer _hpRend = obj.GetComponent<HeartRenderer>();
            if (_hpRend != null)
            {
                char a = obj.name[obj.name.Length - 1];
                int idx = int.Parse(a.ToString()) - 1;
                Debug.Log("idx : " + idx);
                heartRenderers[idx] = _hpRend;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        int oldhp = hp;
        hp -= dmg;

        RenderHp(oldhp, hp);

        if (hp <= 0)
        {
            gameOver.EndGame();
        }

        if (currentIFrame != null)
        {
        } else
        {
            StartCoroutine(IFrame());
            currentIFrame = IFrame();
        }
        
    }

    IEnumerator IFrame()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        // hitSound.Play();
        // shake.CamShakeWithImage();
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        currentIFrame = null;
        triggerCollider.enabled = true;
    }

    private void RenderHp(int oldHp, int newHp)
    {
        if (oldHp > newHp)
        {
            hpStates[newHp] = false;
            foreach(bool st in hpStates)
            {
                Debug.Log(st);
            }
            heartRenderers[newHp].HPLoss();
        }
    }
}