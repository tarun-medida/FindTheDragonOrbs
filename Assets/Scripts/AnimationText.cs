using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationText : MonoBehaviour
{
    public GameObject storyPanel;
    public TMP_Text storyText;
    public string[] dialogues;
    private int index = 0;
    private float wordSpeed = 0.1f;


    void StartTypingText()
    {
        storyPanel.SetActive(true);
        StartCoroutine(Typing());
    }
    IEnumerator Typing()
    {
        storyText.text = dialogues[index];
        yield return null;
    }

    void NextLine()
    {
        if (index < dialogues.Length - 1)
        {
            index++;
            storyText.text = "";
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
