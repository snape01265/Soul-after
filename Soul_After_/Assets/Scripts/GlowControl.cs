using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ư�� ������Ʈ�� ��� ������ �ϴ� ��ũ��Ʈ
public class GlowControl : MonoBehaviour
{
    Material material;
    Color color;
    float timer = 0;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            glow();
            timer = 0;
        }
    }
    private void glow()
    {

        if (color.g >= 200f && color.b >= 200f)
        {
             material.SetColor("_Color", new Color(0f, 10f, 10f));
        }
        else if (color.g <= 200f && color.b <= 200f)
        {
            material.SetColor("_Color", new Color(0f, 255f, 255f));
        }
    }
}
