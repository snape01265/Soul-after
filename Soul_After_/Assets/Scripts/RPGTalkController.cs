using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TriggerEnter -> events 실행 -> fadeTime 만큼 대기
// -> _gameObject의 BoxCollider 설정
public class RPGTalkController : MonoBehaviour
{
    public float fadeTime;
    public GameObject _gameObject;
    public UnityEvent events;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(.01f);
        events.Invoke();
        yield return new WaitForSeconds(fadeTime);
        _gameObject.GetComponent<BoxCollider2D>().enabled = true;   
    }
}
