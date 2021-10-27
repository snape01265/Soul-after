using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 특정 타일이 서서히 나타나도록 하는 효과 부여, 특정 게임 오브젝트가 있음 그거도 활성화 해줌
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
