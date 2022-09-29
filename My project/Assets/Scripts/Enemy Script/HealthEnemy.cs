using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{
    [Header("Health")]
    float maxHealth;
    public float startingHealth;
    public float currentHealth;
    public bool canBeDamaged;

    [Header("Status Effects")]
    public bool isBurning, isShocked, isFrozen;
    public float burnDuration, shockDuration, freezeDuration;
    public float burnProgress, shockProgress, freezeProgress;
    public float burnDamage, shockDamage;           // This Damage Is Per Second 
    private void Awake()
    {
        ResetEnemy();                               // Resets The Enemy's Health And Status Effects To Make Sure It's Ready To Fight!
    }
    void Start()
    {
        canBeDamaged = true;
    }

    void Update()
    {
        Burn();
        Shock();
        Freeze();
    }
    public void callWhenDamagedEnemy(float damage)    // Call This Function Whenever The Enemy Takes Damage
    {
        if (CheckIfDead(damage))
        {
            Debug.Log("Oops, I Have Died With " + currentHealth + " HP Remaining");
            Destroy(this.gameObject);
        }
        else 
        {
            currentHealth = -damage;       
        }
    }
    bool CheckIfDead(float damage)          // Checks If The Enemy Dies After Taking Damage
    {
        if (currentHealth - damage <= 0)
        {
            return true;
        }
        else { return false; }
    }
    
    #region Burn Status
    void CallIfBurned()
    {
        isBurning = true;
        burnProgress = 0f;
    }
    void CallWhileBurning()
    {
        currentHealth -= burnDamage * Time.deltaTime;
        burnProgress += Time.deltaTime;
    }
    void checkIfStillBurned()
    {
        if (burnProgress > burnDuration)
        {
            isBurning= false;
        }
        else { isBurning = true; }
    }
    void Burn()
    {
        checkIfStillBurned();
        if (isBurning)
        {
            CallWhileBurning();
        }
    }
    #endregion Burn Status

    #region Shock Status
    void CallIfShocked()
    {
        isShocked = true;
        shockProgress = 0f;
    }
    void CallWhileShocking()
    {
        currentHealth -= shockDamage * Time.deltaTime;
        shockProgress += Time.deltaTime;
    }
    void checkIfStillShocked()
    {
        if (shockProgress > shockDuration)
        {
            isShocked=false;
        }
        else { isShocked=true; }
    }
    void Shock()
    {
        checkIfStillShocked();
        if (isShocked)
        {
            CallWhileShocking();
        }
    }
    #endregion Shock Status

    #region Freeze Status
    void CallIfFrozen()
    {
        isFrozen = true;
        freezeProgress = 0f;
    }
    void CallWhileFrozen()
    {

    }
    void checkIfStillFrozen()
    {
        if (freezeProgress > freezeDuration)
        {
            isFrozen = false;
        }
        else { isFrozen = true; }
    }
    void Freeze()
    {
        checkIfStillFrozen();
        if (isFrozen)
        {
            CallWhileFrozen();
        }
    }
    #endregion Freeze Status
    void ResetEnemy()
    {
        burnProgress = 0;
        shockProgress = 0;
        freezeProgress = 0;
        isBurning = false;
        isShocked = false;
        isFrozen = false;
        currentHealth = startingHealth;
    }
}