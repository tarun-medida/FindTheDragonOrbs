using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float Health { get;set; }
    public void Hit(float damage);

    public void Hit(float damage, Vector2 push); 
}
