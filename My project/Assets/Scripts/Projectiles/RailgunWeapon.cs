using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunWeapon: Projectile
{
    private GameObject player;
    public LayerMask enemyLayerMask;
    private Transform spawnerForwardShotgun;
    
    protected override void Awake()
    {
        base.Awake();
        spawnerForwardShotgun = ShootManager.instance.forwardShotgun;
    }
    protected override void Start()
    {
        //Vector2 dir = (Vector2)(Quaternion.Euler(0,0,transform.localRotation.z) * Vector2.up);
        RaycastHit2D[] enemyCols = Physics2D.RaycastAll(transform.position,  spawnerForwardShotgun.position - transform.position , 100);
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
        //Debug.Log("Tried To Damage Enemy");
        Debug.Log(ray.collider.gameObject.name + " "+ray.collider.gameObject.layer );
        if ( ray.collider.gameObject.layer == enemyLayer )
        {
            ray.collider.gameObject.GetComponent<HealthEnemy>().CallWhenDamagedEnemy(damage);
            Debug.Log(ray.collider.gameObject.name);
        }
    }

    IEnumerator destroyExterminate()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        //destroyExterminate();
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position +  (Quaternion.Euler(transform.rotation.eulerAngles) * Vector2.up) * 100 ) ;
    }
}
