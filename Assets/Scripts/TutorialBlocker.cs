using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlocker : MonoBehaviour
{
    public GameObject tutorialBlocker;
    public TutorialPopUps popUps;
    public GameObject enterPlayAreaBlocker;
    private bool dummyDead;
    public GameObject objectivePopUp;

    private void Update()
    {
        if(GameObject.Find("Dummy") == null)
        {
            dummyDead = true;
        }
        if(popUps !=null && popUps.enterPlayArea == true && dummyDead)
        {
            Destroy(gameObject);
            // Earlier on activation since IsTrigger was false the player could not go ahead, to solve this enabled IsTrigger for the Play Area Collider
            // Check this script checkIfPlayerHasPassedCollider.cs
            enterPlayAreaBlocker.SetActive(true);
            objectivePopUp.SetActive(true);
        }
        // for levels beyond level 1
        if(GameInstance.instance.getGameData().levelsCompleted > 0)
        {
            Destroy(gameObject);
            // Earlier on activation since IsTrigger was false the player could not go ahead, to solve this enabled IsTrigger for the Play Area Collider
            // Check this script checkIfPlayerHasPassedCollider.cs
            enterPlayAreaBlocker.SetActive(true);
            objectivePopUp.SetActive(true);
        }
    }

}
