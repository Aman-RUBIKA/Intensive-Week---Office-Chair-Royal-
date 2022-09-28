using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float maxHp;
    public float hp;
    public float maxBurnCountdown = 60;
    public float burnCountdown ;
    public float burnDamage;
    public bool freeze = false;
    public bool burn = false;
    public bool shock = false;
    public float explosiveRange = 2;
    private bool isGonnaExplode = false;
    public float explosionCountdown = 10;
    public int type;
    private float step;
    public float speed;
    private float changeSpeed;
    
    public float rangedDistanceStop;
    private Vector2 pPosition;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        burnCountdown = maxBurnCountdown;
        hp = maxHp;
    }

    // Update is called once per frame
    void LateUpdate()
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

        if (hp <= 0)
        {
            Destroy(this.GameObject());
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
