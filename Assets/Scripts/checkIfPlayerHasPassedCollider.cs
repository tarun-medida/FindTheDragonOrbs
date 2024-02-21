using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfPlayerHasPassedCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public bool passed;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            passed = true;
        }
    }
}
