using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : AI
{
    [Header("Melee Attack Variables")]
    [SerializeField] float              attackCooldown;
    [SerializeField] bool               canAttack;

    protected override void Start()
    {
        base.Start();
        ResetVars();
        canAttack = true;
    }
    private void ResetVars()
    {

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (canAttack)
        {
            if (col.gameObject.layer == pcLayer)
            {
                HealthPC.instance.CallWhenDamagedPC(damage);
                Debug.Log("Triggered Hit Against Player");

                StartCoroutine(DelayBeforeAttack());
            }
        }
    }
    IEnumerator DelayBeforeAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
