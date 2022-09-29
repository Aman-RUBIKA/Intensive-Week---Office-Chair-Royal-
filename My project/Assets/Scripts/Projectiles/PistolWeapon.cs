using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : Projectile
{
    private GameObject player;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player.GetComponent<ShootManager>().pistol2)
        {
            canBurn = true;
        }
        else if (player.GetComponent<ShootManager>().pistol1)
        {
            float random = Random.value;
            if (random <= 0.5f)
            {
                canBurn = true;
            }
            else
            {
                
            }
        }
        
        base.Start();
        base.BulletKick();
    }

    void Update()
    {
        
    }
    void CheckCollision(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " and layer " + col.gameObject.layer + " and enemy layer is " + enemyLayer);
        if (col.IsTouchingLayers(wallLayer))
        {
            Debug.Log("Touched A Wall");
            Destroy(this.gameObject);
        }
        else if (col.gameObject.layer==enemyLayer)
        {
            Debug.Log("Touched An Enemy");
            col.gameObject.GetComponent<HealthEnemy>().CallWhenDamagedEnemy(damage);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        CheckCollision(col);
    }
}
