using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunWeapon: Projectile
{
    private GameObject player;
    
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    { 
        Vector2 dir = (Vector2)(Quaternion.Euler(0,0,transform.localRotation.z) * Vector2.right);
        RaycastHit2D[] enemyCols = Physics2D.RaycastAll(transform.position, dir, 20, enemyLayer);
        Debug.Log(dir);
        foreach ( RaycastHit2D ray in enemyCols)
        {
            DamageIfEnemy(ray);
            Debug.Log("collision with enemy");
        }
        base.Start();
        //base.BulletKick();
    }

    void DamageIfEnemy(RaycastHit2D ray)
    {
        if( ray.collider.GetComponent<HealthEnemy>() != null)
        {
            ray.collider.GetComponent<HealthEnemy>().CallWhenDamagedEnemy(damage);
        }
    }

    IEnumerator destroyExterminate()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        destroyExterminate();
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
