using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged_Projectile : Projectile
{
    private Vector2 kickAngleVector;
    private float angleOfKick;
    private GameObject player;
    private static int pcLayerInt = 6;
    public bool isReflected;

    protected override void Awake()
    {
        base.Awake();
    }
    
    protected override void Start()
    {
        player = GameObject.FindWithTag("Player");
        
        base.Start();
        kickAngleVector =new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
        angleOfKick =(Mathf.Atan2(kickAngleVector.y, kickAngleVector.x));
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * angleOfKick) + 90);
        BulletKick();
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        DamageOnCol(col);
    }
    void DamageOnCol(Collider2D col)
    {
        if (isReflected && col.gameObject.layer == enemyLayer)
        {
            col.gameObject.GetComponent<HealthEnemy>().CallWhenDamagedEnemy(base.damage);
            //Destroy(this.gameObject);
        }
        else if (col.gameObject.layer==pcLayerInt)
        {
            HealthPC.instance.callWhenDamagedPC(base.damage);
            //Destroy(this.gameObject);
        }
    }
}
