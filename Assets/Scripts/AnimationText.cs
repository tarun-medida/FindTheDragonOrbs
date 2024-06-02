using System.Collections;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class AnimationText : MonoBehaviour
{
    public GameObject storyPanel;
    public TMP_Text storyText;
    public string[] dialogues;
    private int index = 0;

    private float typeSpeed = 0.1f;
    IEnumerator Typing()
    {
         //storyText.text = dialogues[index];
         index++;
         yield return null;

        // type writing effect
        /*
        int totalVisisbleCharacters = storyText.textInfo.characterCount;
        Debug.Log(storyText.text);
        int counter = 0;
        while(true)
        {
            int visibleCount = counter % (totalVisisbleCharacters + 1);
            storyText.maxVisibleCharacters = visibleCount;
            if(visibleCount >= totalVisisbleCharacters)
            {
                index++;
                break;
            }
            counter++;
            yield return new WaitForSeconds(typeSpeed);
        }     
        */
    }

    void NextLine()
    {
        //if (index < dialogues.Length - 1)
        if(storyPanel.activeSelf == false) 
        {
            storyPanel.SetActive(true);
        }   
        if (index <= dialogues.Length - 1)
        {
            storyText.text = dialogues[index];
            StartCoroutine(Typing());
        }
    }

    void StopTypingText()
    {
        storyText.text = "";
        index = 0;
        storyPanel.SetActive(false);
    }
}
