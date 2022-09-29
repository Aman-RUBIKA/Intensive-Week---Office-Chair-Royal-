using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged_Projectile : Projectile
{
    private Vector2 kickAngleVector;
    private float angleOfKick;
    private GameObject player;

    protected override void Awake()
    {
        
    }
    
    protected override void Start()
    {
        player = GameObject.FindWithTag("Player");
        base.Awake();
        base.Start();
        BulletKick();
        kickAngleVector =new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
        angleOfKick =(Mathf.Atan2(kickAngleVector.y, kickAngleVector.x));
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * angleOfKick) +90);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            HealthPC.instance.callWhenDamagedPC(base.damage);
        }
    }
}
