using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int HP;
    public int maxHP;
    public int SP;
    public int needSP;
    public bool levelClear;
    public GameObject SPDisplay;
    public AudioSource hitSound;
    public AudioSource acqSound;
    public RPGTalk rpgTalk;

    private Gameover gameOver;
    private Text text;
    private CameraShake shake;
    private IEnumerator currentIFrame;
    private IEnumerator currentDamageConvo;

    [Header("Invulnerability Frame")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;
    void Start()
    {
        gameOver = GameObject.Find("GameOver").GetComponent<Gameover>();
        text = SPDisplay.GetComponent<Text>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
    }
    void Update()
    {
        text.text = "SP: " + HP.ToString();
    }
    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Death();
            gameOver.EndGame();
        }
        if (currentIFrame != null)
        {
        }
        else
        {
            StartCoroutine(IFrame());
            currentIFrame = IFrame();
        }
        if (currentDamageConvo != null)
        {
        }
        else
        {
            StartCoroutine(DamageConvo());
            currentDamageConvo = DamageConvo();
        }
    }

    public void AcquireSoul(int soul)
    {
        SP += soul;
        if (SP >= needSP)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<TurnHandler>().patternCount += 1;
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyTurnHandler>().FinishedTurn = true;
            SP = 0;
        }
        acqSound.Play();
    }

    void Death()
    {
        Debug.Log("PlayerDead");
        TurnHandler th = GameObject.FindGameObjectWithTag("GameController").GetComponent<TurnHandler>();
        th.Lost();
    }
    IEnumerator IFrame()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        hitSound.Play();
        shake.CamShakeWithImage();
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
    IEnumerator DamageConvo()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            rpgTalk.NewTalk("hit1_start", "hit1_end", rpgTalk.txtToParse);
        }
        else if (rand == 1)
        {
            rpgTalk.NewTalk("hit2_start", "hit2_end", rpgTalk.txtToParse);
        }
        else if (rand == 2)
        {
            rpgTalk.NewTalk("hit3_start", "hit3_end", rpgTalk.txtToParse);
        }
        else if (rand == 3)
        {
            rpgTalk.NewTalk("hit4_start", "hit4_end", rpgTalk.txtToParse);
        }
        yield return new WaitForSeconds(3.5f);
        currentDamageConvo = null;
    }
}