using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public GameObject attackup;
    public GameObject attackdown;
    public GameObject attackside;
    private Animator animator;
    private void Start()
    {
        // getting player's animator component trigger animations based on key press.
        animator = GetComponent<Animator>();
    }
    public void SpecialBeamAttack(float x, float y)
    {
        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
        animator.SetTrigger("SpecialBeam");
        Invoke("StopAttack", 0.5f);
    }
    private void StopAttack()
    {
        animator.SetTrigger("StopBeam");
    }
}
