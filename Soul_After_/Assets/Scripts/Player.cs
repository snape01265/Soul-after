﻿using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using PixelCrushers.DialogueSystem;

public class Player : MonoBehaviour
{
    [Header("Fox Market Functions")]
    [NonSerialized]
    public int ItemID = 0;
    public ItemProperties[] Items;
    public FloatValue Token;
    public GameObject NoMoneyTab;
    public TokenRenderer tokenRenderer;
    public GameObject menuSet;
    public GameObject askWho;
    public InputField myName;
    public FloatValue curVol;
    public AnimatorOverrideController Suit;
    public AnimatorOverrideController Doc;
    public PlayableDirector timeline;
    public VectorValue startingPosition;
    public BoolValue SuitOn;
    [NonSerialized]
    public bool ispaused = false;
    [NonSerialized]
    public bool inBattle = false;
    [NonSerialized]
    public bool control = true;
    [NonSerialized]
    public float speed = 6.5f;
    [NonSerialized]
    public bool canPause = true;

    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    private readonly float minX = -8;
    private readonly float maxX = 8.05f;
    private readonly float minY = -4.7f;
    private readonly float maxY = 3.65f;
    private readonly float normalVol = 1f;
    private readonly float pauseVol = .6f;
    private GameObject loadSlotMenu;
    private GameObject dialoguePanel;
    private GameObject saveSlotMenu;

    void Start()
    {
        AudioListener.volume = curVol.initialValue * normalVol;
        animator = GetComponent<Animator>();
        dialoguePanel = GameObject.Find("VN Template Standard Dialogue UI_Practice");
        myRigidbody = GetComponent<Rigidbody2D>();
        loadSlotMenu = GameObject.Find("LoadFunction").transform.Find("LoadSlotMenu").gameObject;
        saveSlotMenu = GameObject.Find("UI").transform.Find("Save Slot Menu").gameObject;
        transform.position = startingPosition.initialValue;
        if (SuitOn != null)
        {
            if (SuitOn.initialValue)
                animator.runtimeAnimatorController = Suit;
            else
                animator.runtimeAnimatorController = Doc;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && canPause)
        {
            if (!loadSlotMenu.activeSelf && !saveSlotMenu.activeSelf)
            {
                if (menuSet.activeSelf)
                {
                    menuSet.transform.Find("Sound Settings").gameObject.SetActive(true);
                    menuSet.transform.Find("Option Settings").gameObject.SetActive(true);
                    menuSet.transform.Find("Normal Settings").gameObject.SetActive(true);
                    ResumeGame();
                    menuSet.SetActive(false);                    
                }
                else
                {
                    PauseGame();
                    menuSet.SetActive(true);                   
                }
            }
            else
            {
                loadSlotMenu.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!ispaused)
        {
            if (control)
            {
                change = Vector3.zero;
                change.x = Input.GetAxisRaw("Horizontal");
                change.y = Input.GetAxisRaw("Vertical");
                if (inBattle)
                {
                    Vector3 bounds = transform.position;
                    bounds.x = Mathf.Clamp(bounds.x, minX, maxX);
                    bounds.y = Mathf.Clamp(bounds.y, minY, maxY);

                    transform.position = bounds;
                }
                UpdateAnimationAndMove();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag($"MovableObject"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void ResumeGame()
    {
        if (DialogueManager.IsConversationActive)
        {
            DialogueManager.Unpause();
            GameObject.Find("Continue Button").GetComponent<Button>().interactable = true;
        }      
        Time.timeScale = 1;
        ispaused = false;
        AudioListener.volume = curVol.initialValue * normalVol;
    }

    public void PauseGame()
    {
        if (DialogueManager.IsConversationActive)
        {
            DialogueManager.Pause();
            if (dialoguePanel.transform.Find("Dialogue Panel").gameObject.activeSelf)
            {
                GameObject.Find("Continue Button").GetComponent<Button>().interactable = false;
            }        
        }
        Time.timeScale = 0;
        ispaused = true;
        AudioListener.volume = curVol.initialValue * pauseVol;
    }

    //application quit
    public void GameExit()
    {
        Application.Quit();
    }

    //the player cant move
    public void CancelControl()
    {
        control = false;
        animator.SetBool("Moving", false);
    }

    //give back the controls to player
    public void GiveBackControl()
    {
        control = true;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            // Animation change
            animator.SetFloat("Move_X", change.x);
            animator.SetFloat("Move_Y", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
        // Diagonal movement should be normalized
        if (change.x != 0 && change.y != 0)
        {
            float ms = Mathf.Sqrt(2);
            change.x /= ms;
            change.y /= ms;
        }

        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
            );
    }

    public void SetName()
    {
        askWho.SetActive(true);
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
        CancelControl();
    }

    public void ConfirmName()
    {
        string inputName = myName.text;
        Regex rgx = new Regex("^[a-zA-Z가-힣0-9 ]{1,8}$");
        if (rgx.IsMatch(inputName))
        {
            DialogueLua.SetActorField("Player(NPC)", "Name", inputName);
            askWho.SetActive(false);
            timeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
            GiveBackControl();
        }
        else
        {
            //error message?
        }
    }

    public void ChangeSuit()
    {
        SuitOn.initialValue = true;
        animator.runtimeAnimatorController = Suit;
    }

    public void PlayerLookRight()
    {
        animator.SetFloat("Move_X", 1);
    }

    public void PlayerLookUp()
    {
        animator.SetFloat("Move_Y", 1);
    }

    public void PlayerLookLeft()
    {
        animator.SetFloat("Move_X", -1);
    }

    public void PlayerLookDown()
    {
        animator.SetFloat("Move_Y", -1);
    }

    public void MakePayments()
    {
        if (Token.initialValue > 0 && Token.initialValue >= Items[ItemID].Price)
        {
            GameObject.FindGameObjectWithTag("Market").GetComponent<MarketFunction>().ItemTransaction();
            Token.initialValue -= Items[ItemID].Price;
            tokenRenderer.TokenNo.text = "X " + Token.initialValue.ToString();
            tokenRenderer.BounceToken();
        }
        else if (Token.initialValue <= 0 || Token.initialValue < Items[ItemID].Price)
        {
            NoMoneyTab.SetActive(true);
        }
    }

    public void PayTokenForQuest()
    {
        if (Token.initialValue >= 5)
        {
            Token.initialValue -= 5;
            tokenRenderer.TokenNo.text = "X " + Token.initialValue.ToString();
            GameObject.Find("TokenReward").GetComponent<AudioSource>().Play();
            tokenRenderer.BounceToken();
            DialogueLua.SetVariable("CampFire.TokenPaidToFox", true);
        }
        else
        {
            DialogueLua.SetVariable("CampFire.TokenPaidToFox", false);
        }
    }

    public void FoxGivesTokensBack()
    {
        Token.initialValue += 10;
        tokenRenderer.TokenNo.text = "X " + Token.initialValue.ToString();
        GameObject.Find("TokenReward").GetComponent<AudioSource>().Play();
        tokenRenderer.BounceToken();
    }

    public void DisableCollider()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D col in colliders)
        {
            col.enabled = false;
        }
    }
    public void EnableCollider()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D col in colliders)
        {
            col.enabled = true;
        }
    }
}
