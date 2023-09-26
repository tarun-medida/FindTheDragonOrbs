using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private CharacterDamage character;

    public void UpdateHealth(CharacterDamage characterDamage)
    {
        slider.value = characterDamage.health / characterDamage.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth(character);
    }
}
