using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{

    public GameObject acidEffectPrefab;
    public int Health { get; set; }

    // "public void Start()"
    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Update()
    {
    }

    public void Damage()
    {
        if (isDead == true)
            return;

        Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public override void Movement()
    {
        // Sit still
    }

    public void Attack()
    {
        // Instantiate the acid effect
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }

}
