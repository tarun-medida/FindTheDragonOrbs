using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOrbIfCollected : MonoBehaviour
{
    public GameObject[] Orbs;
    // Start is called before the first frame update
    void Start()
    {
        LoadOrbs();        
    }

    public void LoadOrbs()
    {
        for(int i = 0; i < GameInstance.instance.getGameData().levelsCompleted; i++)
        {
            Orbs[i].gameObject.SetActive(true);
        }
    }

}
