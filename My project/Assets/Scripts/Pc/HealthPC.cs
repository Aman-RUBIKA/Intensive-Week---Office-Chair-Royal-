using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPC : MonoBehaviour
{
    public static HealthPC instance;
    float maxHealth;
    public float startingHealth;
    public float currentHealth;
    public float healAmount;

    public float invincibilityTime;
    public bool canBeDamaged;
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
        startingHealth = currentHealth;
        canBeDamaged = true;
    }

    void Update()
    {
        
    }
    void callWhenDamagedPC(float damage)    // Call This Function Whenever The Player Takes Damage From Anything
    {
        if (CheckIfDead(damage))
        {
            // Add Code Here Once The State Machine Is Complete, To Trigger Game Over
        }
        else 
        {
            if (canBeDamaged)
            {
                StartCoroutine(InvinvibilityAfterDamage());
                currentHealth = -damage;
            }
        }
    }
    void callWhenHealed()                   // Call This Function When The Player Consumes STolen Lunch (Healing Item)
    {
        if (CheckIfHealOverflow())
        {
            currentHealth = maxHealth;
        }
        else { currentHealth += healAmount; }
    }
    bool CheckIfDead(float damage)          // Checks If The Player Dies After Taking Damage
    {
        if (currentHealth - damage <= 0)
        {
            return true;
        }
        else { return false; }
    }
    bool CheckIfHealOverflow()              // Makes Sure The Player Doesn't Heal Over The Maximum Health
    {
        if (currentHealth + healAmount >= maxHealth)
        {
            return true;
        }
        else { return false; }
    }
    IEnumerator InvinvibilityAfterDamage()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(invincibilityTime);
        canBeDamaged = true;
    }


}
