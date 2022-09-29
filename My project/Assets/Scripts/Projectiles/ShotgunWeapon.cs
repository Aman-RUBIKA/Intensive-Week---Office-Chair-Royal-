using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon: Projectile
{
    private GameObject player;
    
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        if (player.GetComponent<ShootManager>().shotgun1)
        {
            float random = Random.value;
            if (random < 0.25f)
            {
                canFreeze = true;
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
        Debug.Log(col.gameObject.name + " and layer " + col.gameObject.layer);
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
        if (canFreeze)
        {
            col.GetComponent<HealthEnemy>().CallIfFrozen();
        }
    }
}
