using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    public bool bePressed;
    public KeyCode keyToPress;
    public GameObject normalEffect, goodEffect, perfectEffect, missEffect;

    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(bePressed)
            {
                gameObject.SetActive(false);
                if (10.75 > Mathf.Abs(transform.position.x) && Mathf.Abs(transform.position.x) > 10.25)
                {
                    GameManager.instance.PerfectHit();
                    Debug.Log("Perfect");
                    Instantiate(perfectEffect, transform.position, goodEffect.transform.rotation);
                }
                else if (11 > Mathf.Abs(transform.position.x) && Mathf.Abs(transform.position.x) > 10)
                {
                    GameManager.instance.GoodHit();
                    Debug.Log("Good");
                    Instantiate(goodEffect, transform.position, normalEffect.transform.rotation);
                }
                else
                {
                    GameManager.instance.NormalHit();
                    Debug.Log("Hit");
                    Instantiate(normalEffect, transform.position, perfectEffect.transform.rotation);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Flower")
        {
            bePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(gameObject.activeSelf)
        {
            if (other.tag == "Flower")
            {
                bePressed = false;

                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
