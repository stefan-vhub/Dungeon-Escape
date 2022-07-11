using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    // "public void Start()"
    public override void Init() 
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        Debug.Log("Moss Giant: Attack()");
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
        }
    }
}