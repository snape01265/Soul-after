using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;

    public GameObject continueButton; 
    private void Start()
    {
        StartCoroutine(Type());
    }
    private void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true); 
        }
    }
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
         
    }
    public void NextSentence()
    {
        continueButton.SetActive(false);
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }

    public void LoadNextScene()
    {
        if (index == sentences.Length - 1)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        
    }


}
