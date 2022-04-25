﻿using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using PixelCrushers.DialogueSystem;

public class Player : MonoBehaviour
{
    [Header("Fox Market Functions")]
    public int ItemID = 0;
    public ItemProperties[] Items;
    public FloatValue Token;
    public RoadBlock road;
    public GameObject menuSet;
    public GameObject askWho;
    public InputField myName;
    public FloatValue curVol;
    public AnimatorOverrideController changeSuit;
    public AnimatorOverrideController changeClothes;
    public AnimatorOverrideController mainClothes;
    public PlayableDirector timeline;
    public VectorValue startingPosition;
    public AnimatorValue animatorValue;
    [NonSerialized]
    public bool ispaused = false;
    [NonSerialized]
    public bool inBattle = false;
    [NonSerialized]
    public bool control = true;
    [NonSerialized]
    public float speed = 20;

    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    private readonly float minX = -8;
    private readonly float maxX = 8.05f;
    private readonly float minY = -4.7f;
    private readonly float maxY = 3.65f;
    private readonly float normalVol = 1f;
    private readonly float pauseVol = 1f;
    private GameObject loadSlotMenu;

    private void Awake()
    {
        if (animatorValue.defaultAnimator != null && animatorValue.initialAnimator != null)
        {
            mainClothes = animatorValue.defaultAnimator;
            changeClothes = animatorValue.initialAnimator;
        }
        else
        {
            animatorValue.defaultAnimator = mainClothes;
            animatorValue.initialAnimator = changeClothes;
        }

        AudioListener.volume = curVol.initialValue * normalVol;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        loadSlotMenu = GameObject.Find("LoadFunction").transform.Find("LoadSlotMenu").gameObject;
        transform.position = startingPosition.initialValue;
    }
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!loadSlotMenu.activeSelf)
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
            AnimatorOverride();
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
        ToggleFunc();
        Time.timeScale = 1;
        ispaused = false;
        AudioListener.volume = curVol.initialValue * normalVol;
    }
    public void PauseGame()
    {
        ToggleFunc();
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
            DialogueLua.SetActorField("Player(NPC)", "Display Name", inputName);
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
        animatorValue.initialAnimator = changeSuit;
        changeClothes = animatorValue.initialAnimator;
    }
    private void AnimatorOverride()
    {
        mainClothes = animatorValue.initialAnimator;
        animator.runtimeAnimatorController = mainClothes as RuntimeAnimatorController;
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
    private void ToggleFunc()
    {
        RPGTalk[] rPGTalks = GameObject.FindObjectsOfType<RPGTalk>();
        RPGTalkArea[] rPGTalkAreas = GameObject.FindObjectsOfType<RPGTalkArea>();

        foreach (RPGTalk rpg in rPGTalks)
        {
            rpg.TogglePause();
        }

        foreach (RPGTalkArea rpgarea in rPGTalkAreas)
        {
            rpgarea.TogglePause();
        }
    }
    public void MakePayments()
    {
        if (Token.initialValue > 0 && Token.initialValue > Items[ItemID].Price)
        {
                GameObject.FindGameObjectWithTag("Market").GetComponent<MarketFunction>().ItemTransaction();
                Token.initialValue -= Items[ItemID].Price;
                Debug.Log("You bought an item!");
        }
        else if (Token.initialValue < 0 || Token.initialValue < Items[ItemID].Price)
        {
                //need a UI for this
                Debug.Log("Not enough money!");
        }
    }   
}
