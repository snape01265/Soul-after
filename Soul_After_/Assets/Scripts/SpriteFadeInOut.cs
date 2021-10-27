using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Ư�� Ÿ���� ������ ��Ÿ������ �ϴ� ȿ�� �ο�, Ư�� ���� ������Ʈ�� ���� �װŵ� Ȱ��ȭ ����
public class SpriteFadeInOut : MonoBehaviour
{
    private Tilemap tm;
    public GameObject Obj;

    private void Start()
    {
        tm = GetComponent<Tilemap>();
    }

    public void SpriteFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    public void SpriteFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        Color targetColor = new Color(tm.color.r, tm.color.g, tm.color.b, 1);
        while (1 - tm.color.a >= .01f)
        {
            tm.color = Color.Lerp(tm.color, targetColor, .05f);
            yield return null;
        }

        if(Obj)
            Obj.SetActive(true);
        
        yield return null;
    }

    IEnumerator FadeOut()
    {
        Color targetColor = new Color(tm.color.r, tm.color.g, tm.color.b, 0);
        
        if(Obj)
            Obj.SetActive(false);
        
        while (1 - tm.color.a <= .01f)
        {
            tm.color = Color.Lerp(tm.color, targetColor, .05f);
            yield return null;
        }
    }
}
