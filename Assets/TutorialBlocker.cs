using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlocker : MonoBehaviour
{
    public GameObject tutorialBlocker;
    public TutorialPopUps popUps;

    private void Update()
    {
        if(popUps.enterPlayArea == true)
        {
            Destroy(gameObject);
        }
    }
}
