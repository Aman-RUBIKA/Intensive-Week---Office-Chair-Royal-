using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class AI : MonoBehaviour
{
    [Header("MidBoss Variables")]
    public bool midBoss;
    public bool isCharging;
    public float chargeSpeed;
    private Vector2 targetPos;
    public float chargeMaxTimer = 10;
    public float chargeTimer;
    public bool rangedTrajectory;
    public bool minesDeployed;
    public LineRenderer lr;

    [Header("General Variables")]
    public float maxHp;
    public float hp;
    public float maxBurnCountdown = 60;

    [Header("Status Effects")]
    public float burnCountdown ;
    public float burnDamage;
    public bool freeze = false;
    public bool burn = false;
    public bool shock = false;

    [Header("Enemy Parameters")]
    public float explosiveRange = 2;
    private bool isGonnaExplode = false;
    public float explosionCountdown = 10;
    public int type;
    private float step;
    public float speed;
    private float changeSpeed;

    public float shootTimerMax;
    [SerializeField] 
    float  shootTimer;
    
    public float rangedDistanceStop;
    public Vector2 pPosition;
    private GameObject player;
    public GameObject bulletPrefab;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        burnCountdown = maxBurnCountdown;
        hp = maxHp;
        shootTimer = shootTimerMax;
        chargeTimer = chargeMaxTimer;
        chargeSpeed = speed * 3;
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shock)
        {
            changeSpeed = 0.5f;
        }
        if (freeze)
        {
            changeSpeed = 0;
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
        if(burn == false && freeze == false && shock == false)
        {
            changeSpeed = 1;
        }
        
        pPosition = player.transform.position;
        step = speed * Time.deltaTime * changeSpeed;

        if (midBoss)
        {
            switch (type)
            {
                case 0: // Charge mid Boss
                    MidBossCharge();
                    break;
                case 1: // Range mid Boss
                    break;
                case 2: // Explosive mid boss
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case 0 :
                    MeleeSeek();
                    break;
                case 1:
                    RangedSeek();
                    break;
                case 2:
                    ExplosiveSeek();
                    break;
            } 
        }
        
        if (hp <= 0)
        {
            Destroy(this.GameObject());
        }
        
    }

    void MidBossCharge()
    {
        if (isCharging == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, pPosition, step);
            TurnToPlayer();
            if (Vector2.Distance(transform.position, pPosition) < 10)
            {
                isCharging = true;
                targetPos = pPosition;
            }
        }
        else
        {
            if (chargeTimer <= 0)
            {
                // Charge towards position
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPos.x,  + targetPos.y), chargeSpeed * Time.deltaTime * changeSpeed);
                if (Vector2.Distance(transform.position, targetPos) == 0)
                {
                    isCharging = false;
                    chargeTimer = chargeMaxTimer;
                }
            }
            else
            {
                chargeTimer -= Time.deltaTime;
                lr.startColor = Color.red;
                lr.endColor = Color.red;
                lr.positionCount = 2;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1,targetPos);

            }
        }
    }

    void ExplosiveSeek()
    {
        if (Vector2.Distance(transform.position, pPosition) < explosiveRange)
        {
            isGonnaExplode = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, pPosition, step);  
        

        if (isGonnaExplode)
        {
            if (explosionCountdown > 0)
            {
                explosionCountdown -= Time.deltaTime;  
            }
            else
            {
                Debug.Log("explosion go brrr");
            }
        }
        TurnToPlayer();
    }
    
    void RangedSeek()
    {
        TurnToPlayer();
        if (Vector2.Distance(transform.position, pPosition) > rangedDistanceStop)
        {
            transform.position = Vector2.MoveTowards(transform.position, pPosition, step); 
        }
        else
        {
            if (shootTimer <= 0)
            {
                Instantiate(bulletPrefab, transform.position, transform.localRotation);
                shootTimer = shootTimerMax;
            }
            else
            {
                shootTimer -= Time.deltaTime;
            }
        }
        
    }
    
    void MeleeSeek()
    {
        TurnToPlayer();
        transform.position = Vector2.MoveTowards(transform.position, pPosition, step);
    }

    void TurnToPlayer()
    {
        float angle;
        angle =(Mathf.Atan2(transform.position.y - pPosition.y, transform.position.x - pPosition.x));
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * angle) - 90);
    }

}
