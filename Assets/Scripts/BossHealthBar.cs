using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private CharacterDamage character;

    public void UpdateHealth(float currHealth,float maxHealth)
    {
        slider.value = currHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
