using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : Projectile
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        base.BulletKick();
    }

    void Update()
    {
        
    }
    void CheckCollision(Collider2D col)
    {
        if (col.IsTouchingLayers(wallLayer))
        {
            Debug.Log("Touched A Wall");
            Destroy(this.gameObject);
        }
        else if (col.IsTouchingLayers(enemyLayer))
        {
            Debug.Log("Touched An Enemy");
            // Call Enemy's If Damaged Script Here
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        CheckCollision(col);
    }
}