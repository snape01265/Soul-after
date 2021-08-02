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
    public AudioSource hitSound;
    public AudioSource acqSound;

    [Header("Invulnerability Frame")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;
    void Start()
    {
        text = SPDisplay.GetComponent<Text>();
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
