using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunWeapon : Projectile
{

    private GameObject player;
    
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player.GetComponent<ShootManager>().mach2)
        {
            RandomBulletDirection(3);
        }
        else if (player.GetComponent<ShootManager>().mach1)
        {
            float random = Random.value;
            if (random < 0.15f)
            {
                canStun = true;
            }
        }
        
        base.Start();
        base.RandomBulletDirection(1.5f);
        base.BulletKick();
    }

    void Update()
    {
        
    }
    void CheckCollision(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " and layer " + col.gameObject.layer);
        if (col.IsTouchingLayers(wallLayer))
        {
            Debug.Log("Touched A Wall");
            Destroy(this.gameObject);
        }
        else if (col.gameObject.layer == enemyLayer)
        {
            Debug.Log("Touched An Enemy");
            col.gameObject.GetComponent<HealthEnemy>().CallWhenDamagedEnemy(damage);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        CheckCollision(col);
        if (canStun)
        {
            col.gameObject.GetComponent<HealthEnemy>().CallIfShocked();
        }
    }
}
