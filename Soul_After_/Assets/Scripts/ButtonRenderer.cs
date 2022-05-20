using UnityEngine;

public class ButtonRenderer : MonoBehaviour
{
    private SajaPuzzleBehavior saja;
    private bool BtnState;
    private int BtnIdx;
    public AudioSource _audio;
    private Animator anim;
    private void Awake()
    {
        saja = this.gameObject.GetComponentInParent<SajaPuzzleBehavior>();
        anim = GetComponent<Animator>();
        char a = this.gameObject.name[this.gameObject.name.Length - 1];
        BtnIdx = int.Parse(a.ToString()) - 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // disable Button Up Sprite
        // also change ButtonState accordingly
        if (collision.GetComponent<Collider2D>().CompareTag($"MovableObject"))
        {
            saja.ToggleBoxOnBtn(BtnIdx);
            if(anim.GetBool("Pressed") == false)
            {
                ButtonDown();
                gameObject.GetComponentInParent<SajaPuzzleBehavior>().AdvancePuzzle(BtnIdx);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag($"MovableObject"))
        {
            saja.ToggleBoxOnBtn(BtnIdx);
        }
    }

    public void ButtonUp()
    {
        _audio.Play();
        anim.SetBool("Pressed", false);
    }

    public void ButtonDown()
    {
        _audio.Play();
        anim.SetBool("Pressed", true);
    }
}
