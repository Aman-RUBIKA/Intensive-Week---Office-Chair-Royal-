using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    public static Kick instance;
    private Collider2D[] hitEnemyList;
    public BoxCollider2D kickHitbox;
    public Vector2 kickHitboxVector;
    public LayerMask enemyHitLayer;
    public float kickStrength;

    private void Awake()
    {
        #region Simpleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion Simpleton
    }
    void Start()
    {
        kickHitboxVector = new Vector2(kickHitbox.size.x, kickHitbox.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PerformKick()
    {
        hitEnemyList = Physics2D.OverlapBoxAll(transform.position + Vector3.up, kickHitboxVector, 0, enemyHitLayer);
        foreach (Collider2D col in hitEnemyList)
        {
            // Add Code Here Once You Create An Enemy Health System
            col.attachedRigidbody.AddForce(-PcController.instance.kickAngleVector.normalized * kickStrength, ForceMode2D.Impulse);
            Debug.Log(col.gameObject.name);
        }
        System.Array.Clear(hitEnemyList, 0, hitEnemyList.Length);
    }
}
