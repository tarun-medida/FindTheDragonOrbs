using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlocker : MonoBehaviour
{
    public GameObject tutorialBlocker;
    public TutorialPopUps popUps;
    public GameObject enterPlayAreaBlocker;
    private bool dummyDead;
    private GameObject getDummy;

    private void Update()
    {
        getDummy = GameObject.Find("Dummy");
        if(getDummy == null)
        {
            dummyDead = true;
        }
        if(popUps.enterPlayArea == true && dummyDead)
        {
            Destroy(gameObject);
            // Earlier on activation since IsTrigger was false the player could not go ahead, to solve this enabled IsTrigger for the Play Area Collider
            // Check this script  checkIfPlayerHasPassedCollider.cs
            enterPlayAreaBlocker.SetActive(true);
            
        }
    }

}