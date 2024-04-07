using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public string AudioName;
    void Start()
    {
        AudioManager.instance.PlaySFX(AudioName);
    }

}
