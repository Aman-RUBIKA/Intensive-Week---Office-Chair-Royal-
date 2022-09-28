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
    void IDontKnow()
    {
        
        if (freeze)
        {
            changeSpeed = 0;
        }
        else if (shock)
        {
            changeSpeed = 0.5f;
        }
        if (burn || shock)
        {
            if (burnCountdown <= 0)
            {
                burnCountdown = maxBurnCountdown;
                hp -= burnDamage;
            }
            else
            {
                burnCountdown -= Time.deltaTime;
            }

            changeSpeed = 1;
        }
        if (burn == false && freeze == false && shock == false)
        {
            changeSpeed = 1;
        }
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

    }
    void CallWhileShocking()
    {

    }
    bool checkIfStillShocked()
    {
        if (burnProgress > burnDuration)
        {
            return false;
        }
        else { return true; }
    }
    #endregion Shock Status

    #region Freeze Status
    void CallIfFrozen()
    {

    }
    void CallWhileFrozen()
    {

    }
    bool checkIfStillFrozen()
    {
        if (burnProgress > burnDuration)
        {
            return false;
        }
        else { return true; }
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
