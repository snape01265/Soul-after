using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class TokenReward : MonoBehaviour
{
    private AudioSource TokenSFX;
    private TokenRenderer tokenRenderer;
    private int reward;

    // Start is called before the first frame update
    void Start()
    {
        tokenRenderer = GameObject.FindGameObjectWithTag("Token").GetComponent<TokenRenderer>();
        TokenSFX = gameObject.GetComponent<AudioSource>();
    }

    public void DefaultReward()
    {
        reward = 2;
        TokenUIChange();
        Debug.Log("DefaultReward method successful");
    }

    public void RhythmGameReward()
    {
        string rankValue = DialogueLua.GetVariable("RhythmGame.RankValue").asString;

        switch (rankValue)
        {
            case "F":
                reward = 0;
                TokenUIChange();
                Debug.Log("Reward according to F.");
                break;

            case "D":
                reward = 1;
                TokenUIChange();
                Debug.Log("Reward according to D.");
                break;

            case "C":
                reward = 2;
                TokenUIChange();
                Debug.Log("Reward according to C.");
                break;

            case "B":
                reward = 3;
                TokenUIChange();
                Debug.Log("Reward according to B.");
                break;
            
            case "A":
                reward = 4;
                TokenUIChange();
                Debug.Log("Reward according to A.");
                break;

            case "S":
                reward = 5;
                TokenUIChange();
                Debug.Log("Reward according to S.");
                break;

            default:
                reward = 0;
                DialogueLua.SetVariable("RhythmGame.RankValue", null);
                Debug.Log("Nothing happens!");
                break;
        }
        // Reset the variable after the reward by running lua code.
        DialogueLua.SetVariable("RhythmGame.RankValue", null);
    }

    public void ShootingGameReward()
    {
        bool isClassicMode = DialogueLua.GetVariable("Defense_Classic_Mode").asBool;
  
        if (isClassicMode == true)
        {
            bool clearClassic = DialogueLua.GetVariable("DefenseGame.ClassicModeClear").asBool;

            if (clearClassic)
            {
                reward = 3;
                TokenUIChange();
            }
            DialogueLua.SetVariable("DefenseGame.ClassicModeClear", false);
        }
        else
        {
            int WaveCount = DialogueLua.GetVariable("DefenseGame.WaveCount").asInt;
            // fix the denominator when needed
            tokenRenderer.Token.initialValue += WaveCount / 6;
            tokenRenderer.TokenNo.text = "X " + tokenRenderer.Token.initialValue.ToString();
            tokenRenderer.BounceToken();
            TokenSFX.Play();
            DialogueLua.SetVariable("DefenseGame.WaveCount", 0);
        }
    }

    private void TokenUIChange()
    {
        tokenRenderer.Token.initialValue += reward;
        tokenRenderer.TokenNo.text = "X " + tokenRenderer.Token.initialValue.ToString();
        tokenRenderer.BounceToken();
        TokenSFX.Play();
    }
}
