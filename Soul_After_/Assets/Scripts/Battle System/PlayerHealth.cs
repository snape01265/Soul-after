using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int hp;
    public int maxHP;
    public bool levelClear;
    public AudioSource hitSound;
    public AudioSource acqSound;
    public RPGTalk rpgTalk;

    private Gameover gameOver;
    private CameraShake shake;
    private IEnumerator currentIFrame;

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
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
    }

    public void TakeDamage(int dmg)
    {
        int oldhp = hp;
        hp -= dmg;


        if (hp <= 0)
        {
            gameOver.EndGame();
        }

        if (currentIFrame == null)
        {
            StartCoroutine(IFrame());
            currentIFrame = IFrame();
        }
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
}