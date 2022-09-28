using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool canFreeze;
    public bool canBurn;
    public bool canStun;
    
    protected Rigidbody2D projectileRB;
    public Transform parentT;
    public LayerMask enemyLayer;
    public LayerMask wallLayer;

    [Header("Bullet Variables")]
    public float projectileSpeed = 10f;
    public float projectileRange = 10f;
    public float bulletAngle;
    public float lifeSpan = 2f;
    public int damage = 1;

    protected virtual void Awake()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        parentT = GetComponentInParent<Transform>().GetComponentInParent<Transform>();
    }
    protected virtual void Start()
    {
        Destroy(this.gameObject, lifeSpan);
        projectileRB.gravityScale = 0;
    }
    protected internal void BulletKick()
    {
        projectileRB.velocity=(transform.up * projectileSpeed);
    }
    public void RandomBulletDirection(float randomRange)
    {
        Debug.Log("Determine Bullet Direction Is Being Called");
        bulletAngle = (UnityEngine.Random.Range(-randomRange, randomRange));
        //Debug.Log(bulletAngle + " Is The Bullet's Angle Randomness");
        //bulletAngle += PcController.instance.pivotFireTransform.transform.rotation.z;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + bulletAngle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual internal void Init(GameObject parent)
    {
        //this.parent = parent;

    }


    public void StatusEffect(Collider2D col)
    {
        if (col.GetComponent<AI>() != null)
        {
            if (canFreeze)
            {
                col.GetComponent<AI>().freeze = true;
            }

            if (canStun)
            {
                col.GetComponent<AI>().shock = true;
            }

            if (canBurn)
            {
                col.GetComponent<AI>().burn = true;
                col.GetComponent<AI>().burnCountdown = col.GetComponent<AI>().maxBurnCountdown;
            }
        }
    }
}
