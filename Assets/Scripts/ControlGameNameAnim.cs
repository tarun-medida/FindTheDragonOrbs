using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlGameNameAnim : MonoBehaviour
{
    public GameObject gameNameContainer;
    public TMP_Text gameNameText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableGameTitleScreen());
    }



    IEnumerator DisableGameTitleScreen()
    {
        float delay = 2.5f;
        float animationLength = gameNameText.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength + delay);
        gameNameText.text = "";
        gameNameContainer.GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(1f);
        gameNameContainer.SetActive(false);
    }
}
