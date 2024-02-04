using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // getting player's animator component trigger animations based on key press.
        animator = GetComponent<Animator>();
    }
    public void GetMoveInput(float x , float y)
    {
        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }
    public void SpecialBeamAttack()
    {
        animator.SetTrigger("SpecialBeam");
        Invoke("StopAttack", 0.5f);
    }
    private void StopAttack()
    {
        animator.SetTrigger("StopBeam");
    }
}
