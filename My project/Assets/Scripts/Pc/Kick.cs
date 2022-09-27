using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    private Collider2D[] hitEnemyList;
    public BoxCollider2D kickHitbox;
    public Vector2 kickHitboxVector;
    public LayerMask enemyHitLayer;
    public float kickStrength;
    // Start is called before the first frame update
    void Start()
    {
        kickHitboxVector = new Vector2(kickHitbox.size.x, kickHitbox.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PerformKick()
    {
        hitEnemyList = Physics2D.OverlapBoxAll(transform.position + Vector3.up, kickHitboxVector, 0, enemyHitLayer);
        foreach (Collider2D col in hitEnemyList)
        {
            // Add Code Here Once You Create An Enemy Health System
            //col.attachedRigidbody.AddForce(Vector2 )
                
        }
    }
}
