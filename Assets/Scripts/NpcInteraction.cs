using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NpcInteraction : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text npcText;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerInRange;
    public GameObject button;

 // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange) 
        {

            if(dialoguePanel.activeInHierarchy) {
                ZeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        } 
        if(npcText.text == dialogue[index])
        {
            button.SetActive(true);
        }
    }

    public void ZeroText()
    {
        npcText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char c in dialogue[index].ToCharArray())
        {
            npcText.text += c; 
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    public void NextLine()
    {
        button.SetActive(false);

        if(index<dialogue.Length-1)
        {
            index++;
            npcText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to talk to the npc");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            ZeroText();
        }
    }


}
