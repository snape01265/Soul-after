using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System;

public class Player : MonoBehaviour
{
    public float speed;
    public bool control;
    public Quest quest;
    public RoadBlock road;
    public GameObject menuSet;
    public GameObject askWho;
    public InputField myName;
    public RPGTalk rpgTalk;
    public AnimatorOverrideController changeSuit;
    public AnimatorOverrideController changeClothes;
    public AnimatorOverrideController mainClothes;
    public TimelinePlayer timeline;

    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    private bool nameSet;
    private static bool ispaused = false;

    public VectorValue startingPosition;
    public BoolValue nameSetValue;
    public StringValue nameSave;
    public AnimatorValue animatorValue;

    private void Awake()
    {
        if(animatorValue.defaultAnimator != null && animatorValue.initialAnimator != null)
        {
            mainClothes = animatorValue.defaultAnimator;
            changeClothes = animatorValue.initialAnimator;
        }
        else
        {
        animatorValue.defaultAnimator = mainClothes;
        animatorValue.initialAnimator = changeClothes;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
        nameSet = nameSetValue.initialValue;
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
            {
                ResumeGame();
                menuSet.SetActive(false);
            }
            else
            {
                PauseGame();
                menuSet.SetActive(true);
            }
        }
        if (!ispaused)
        {
            if (control)
            {
                change = Vector3.zero;
                change.x = Input.GetAxisRaw("Horizontal");
                change.y = Input.GetAxisRaw("Vertical");
                UpdateAnimationAndMove();
            }
            if (rpgTalk != null)
            {
                AssignNameVariable();
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
        Time.timeScale = 1;
        ispaused = false;
        AudioListener.volume = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        ispaused = true;
        AudioListener.volume = 0.25f;
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
    public void ConditionalGiveBackControl()
    {
        if (nameSet)
        {
            control = true;
        }
    }
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            // 이하 Animation 값 조정
            animator.SetFloat("Move X", change.x);
            animator.SetFloat("Move Y", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
    void MoveCharacter()
    {
        // 대각선 움직임이 두배의 속도를 만드는 문제 해결
        if (change.x != 0  && change.y != 0)
        {
            float ms = Mathf.Sqrt(2);
            change.x /= ms;
            change.y /= ms;
        }

        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
            );
    }
    public void WhoAreYou()
    {
        if (nameSet == false)
        {
            askWho.SetActive(true);
            myName.Select();
            control = false;
            animator.SetBool("Moving", false);
        }
    }
    public void SetName()
    {
        askWho.SetActive(false);
        nameSave.initialValue = (string) myName.text;
        AssignNameVariable();
        nameSet = true;
        nameSetValue.initialValue = nameSet;
        timeline.StartTimeline();
    }
    private void AssignNameVariable()
    {
        rpgTalk.variables[0].variableValue = nameSave.initialValue;
    }
    public void ChangeSuit()
    {
        animatorValue.initialAnimator = changeSuit;
        animatorValue.defaultAnimator = changeSuit;
        changeClothes = animatorValue.initialAnimator;
    }
    private void AnimatorOverride()
    {
        mainClothes = animatorValue.initialAnimator;
        animator.runtimeAnimatorController = mainClothes as RuntimeAnimatorController;
    }
    public void QuestProgress0()
    {
        if (quest.isActive)
        {
            quest.goal.NPC0Talked();
            if (quest.goal.IsReached())
            {
                quest.Complete();
                road.QuestTrigger();
            }
        }
    }
    public void QuestProgress1()
    {
        if (quest.isActive)
        {
            quest.goal.NPC1Talked();
            if (quest.goal.IsReached())
            {
                quest.Complete();
                road.QuestTrigger();
            }
        }
    }
    public void QuestProgress2()
    {
        if (quest.isActive)
        {
            quest.goal.NPC2Talked();
            if (quest.goal.IsReached())
            {
                quest.Complete();
                road.QuestTrigger();
            }
        }
    }
    public void QuestProgress3()
    {
        if (quest.isActive)
        {
            quest.goal.NPC3Talked();
            if (quest.goal.IsReached())
            {
                quest.Complete();
                road.QuestTrigger();
            }
        }
    }
}
