using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthPC : MonoBehaviour
{
    public static HealthPC instance;
    public float maxHealth;
    public float startingHealth;
    public float currentHealth;
    public float healAmount;
    public Healthbar healthBar;
    public float invincibilityTime;
    public bool canBeDamaged;
    public GameObject deathCanvas;
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
        //Debug.Log(startingHealth);
        currentHealth=startingHealth;
        //Debug.Log(currentHealth);
        canBeDamaged = true;
    }

    void Update()
    {
        
    }
    public void CallWhenDamagedPC(float damage)    // Call This Function Whenever The Player Takes Damage From Anything
    {
        //Debug.Log(currentHealth);
        currentHealth -= damage;
        //Debug.Log(currentHealth);
        if (IsDead())
        {
            // Add Code Here Once The State Machine Is Complete, To Trigger Game Over
            Death();
        }
        else 
        {
            if (canBeDamaged)
            {
                healthBar.SetHealth(currentHealth);
                StartCoroutine(InvinvibilityAfterDamage());
            }
        }
    }
    public void CallWhenHealed()                   // Call This Function When The Player Consumes STolen Lunch (Healing Item)
    {
        if (CheckIfHealOverflow())
        {
            currentHealth = maxHealth;
        }
        else { currentHealth += healAmount; }
    }
    bool IsDead()          // Checks If The Player Dies After Taking Damage
    {
        if (currentHealth <= 0)
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

    public void Death()
    {
        deathCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

}
