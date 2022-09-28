using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged_Projectile : Projectile
{
    
    protected override void Awake()
    {
        
    }
    
    protected override void Start()
    {
        
        base.Awake();
        base.Start();
        BulletKick();
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            HealthPC.instance.callWhenDamagedPC(base.damage);
        }
    }
}
