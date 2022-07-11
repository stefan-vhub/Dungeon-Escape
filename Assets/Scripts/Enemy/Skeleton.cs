using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    // "public void Start()"
    public override void Init()
    {
        base.Init();
        Health = base.health; // Assign Health proprety to our enemy health
    }

    public void Damage()
    {
        Debug.Log("Skeleton: Attack()");
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if(Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
        }
    }
}
