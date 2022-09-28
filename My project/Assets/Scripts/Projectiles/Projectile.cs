using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        projectileRB.AddForce(transform.up * projectileSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual internal void Init(GameObject parent)
    {
        //this.parent = parent;

    }
}
