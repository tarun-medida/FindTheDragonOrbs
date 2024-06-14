using System.Collections;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class AnimationText : MonoBehaviour
{
    public GameObject storyPanel;
    public TMP_Text storyText;
    public string[] dialogues;
    private int index = 0, totalVisisbleCharacters,counter = 0;
    public Animator cutsceneAnimator;

    private float typeSpeed = 0.1f;
    IEnumerator Typing()
    {
         
        index++;
        yield return null;
         

        // type writing effect
        /*
        totalVisisbleCharacters = storyText.textInfo.characterCount;
        Debug.Log(totalVisisbleCharacters);
        Debug.Log(storyText.text);
        counter = 0;
        while(true)
        {
            int visibleCount = counter % (totalVisisbleCharacters + 1);
            Debug.Log(counter);
            Debug.Log(visibleCount);
            Debug.Log(totalVisisbleCharacters);
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
    void PauseCutScene()
    {
        StartCoroutine(PauseScene());
    }

    IEnumerator PauseScene()
    {
        cutsceneAnimator.speed = 0f;
        yield return new WaitForSeconds(2f);
        cutsceneAnimator.speed = 1f;
    }

    void NextLine()
    {
        //if (index < dialogues.Length - 1)

        if (index < dialogues.Length)
        {
            if (storyPanel.activeSelf == false)
            {
                storyPanel.SetActive(true);
            }
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
