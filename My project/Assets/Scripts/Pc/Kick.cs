using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    public static Kick instance;
    private Collider2D[] hitEnemyList;
    public BoxCollider2D kickHitbox;
    public Vector2 kickHitboxVector;
    public Vector3 kickHitBoxOffset;
    [SerializeField]
    private LayerMask enemyHitLayer;
    public float kickStrength;
    Vector3 orientAngle;

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
        kickHitBoxOffset = new Vector3(kickHitbox.offset.x, kickHitbox.offset.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        orientAngle = PcController.instance.gameObject.transform.localEulerAngles;
    }
    public void PerformKick()
    {
        Debug.Log("Performing Kick");
        hitEnemyList = Physics2D.OverlapBoxAll(transform.position + kickHitBoxOffset, kickHitboxVector, orientAngle.z, enemyHitLayer);

        if (hitEnemyList.Length > 0)
        {
            Debug.Log(hitEnemyList[0].gameObject.layer);
        }

        foreach (Collider2D col in hitEnemyList)
        {
            // Add Code Here Once You Create An Enemy Health System
            col.attachedRigidbody.AddForce(-PcController.instance.kickAngleVector.normalized * kickStrength, ForceMode2D.Impulse);
            Debug.Log(col.gameObject.name);
        }
        System.Array.Clear(hitEnemyList, 0, hitEnemyList.Length);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + kickHitBoxOffset, kickHitboxVector);
    }
}
