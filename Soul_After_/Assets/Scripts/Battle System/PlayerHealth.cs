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
    private Text text;
    private CameraShake shake;
    public AudioSource hitSound;
    public AudioSource acqSound;
    private Gameover gameOver;

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
        if(HP <= 0)
        {
            Death();
            gameOver.EndGame();
        }
        StartCoroutine(IFrame());
    }

    public void AcquireSoul(int soul)
    {
        SP += soul;
        if (SP >= needSP)
        {
            levelClear = true;
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
        while(temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
}
