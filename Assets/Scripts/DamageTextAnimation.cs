using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextAnimation : MonoBehaviour
{

    public float destroyTime = 3f;
    private Vector3 offset = new Vector3(0,4.43f,0);
    private Vector3 randomPos = new Vector3(0.5f,0,0);
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,destroyTime);
        gameObject.GetComponent<RectTransform>().localPosition += offset;
        gameObject.GetComponent<RectTransform>().localPosition += new Vector3(Random.Range(-randomPos.x, randomPos.x), Random.Range(-randomPos.y, randomPos.y), Random.Range(-randomPos.z, randomPos.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
