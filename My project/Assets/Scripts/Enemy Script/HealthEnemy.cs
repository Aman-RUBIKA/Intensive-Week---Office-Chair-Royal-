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
    public bool isBurning, isShocked, isFrozen = false;
    public float burnDuration, shockDuration, freezeDuration;
    public float burnProgress, shockProgress, freezeProgress;
    public float burnDamage, shockDamage;           // This Damage Is Per Second 

    public GameObject goldManager;
    private void Awake()
    {
        ResetEnemy();                               // Resets The Enemy's Health And Status Effects To Make Sure It's Ready To Fight!
    }
    void Start()
    {
        goldManager = GameObject.FindWithTag("GoldManager");
        ResetEnemy();
        canBeDamaged = true;
    }

    void Update()
    {
        Burn();
        Shock();
        Freeze();
    }
    public void CallWhenDamagedEnemy(float damage)    // Call This Function Whenever The Enemy Takes Damage
    {
        if (currentHealth <= 0)
        {
            return;
        }
        Debug.Log(damage + " is the Damage I've Taken");
        if (CheckIfDead(damage))
        {
            goldManager.GetComponent<GoldManager>().AddGold(3);
            
            Debug.Log("Oops, I Have Died With " + currentHealth + " HP Remaining");
            GameObject manager = GameObject.FindWithTag("EnemyManager");
            int type = transform.GetComponent<AI>().type;
            if (transform.GetComponent<AI>().midBoss)
            {
                type = 0;
                manager.GetComponent<EnemyManager>().bossPresent = false;
                //manager.Get
            }
            manager.GetComponent<EnemyManager>().enemyKilled(type);
            GameObject dropManager = GameObject.FindWithTag("EnemyDropManager");
            dropManager.GetComponent<EnemyDropmanager>().canAppearItem = true;
            dropManager.GetComponent<EnemyDropmanager>().appearFood(new Vector2(transform.position.x, transform.position.y));
            //Spaghetti code, I know, don't worry about it, it works
            
            Destroy(this.gameObject);
        }
        else 
        {
            currentHealth -= damage;
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
    public void CallIfBurned()
    {
        isBurning = true;
        burnProgress = 0f;
    }
    void CallWhileBurning()
    {
        currentHealth -= burnDamage * Time.deltaTime;
        burnProgress += Time.deltaTime;
        if (currentHealth <= 0)
        {
            CallWhenDamagedEnemy(0);
        }
    }
    void checkIfStillBurned()
    {
        if (burnProgress >= burnDuration)
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
    public void CallIfShocked()
    {
        isShocked = true;
        shockProgress = 0f;
    }
    void CallWhileShocking()
    {
        currentHealth -= shockDamage * Time.deltaTime;
        shockProgress += Time.deltaTime;
        if (currentHealth <= 0)
        {
            CallWhenDamagedEnemy(0);
        }
    }
    void checkIfStillShocked()
    {
        if (shockProgress >= shockDuration)
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
    public void CallIfFrozen()
    {
        isFrozen = true;
        freezeProgress = 0f;
    }
    void CallWhileFrozen()
    {

    }
    void checkIfStillFrozen()
    {
        if (freezeProgress >= freezeDuration)
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
        burnProgress = burnDuration;
        shockProgress = shockDuration;
        freezeProgress = freezeDuration;
        isBurning = false;
        isShocked = false;
        isFrozen = false;
        currentHealth = startingHealth;
    }
}
