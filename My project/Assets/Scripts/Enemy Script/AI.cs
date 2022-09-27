using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float explosiveRange = 2;
    private bool isGonnaExplode = false;
    public float explosionCountdown = 10;
    public int type;
    private float step;
    public float speed;
    public float rangedDistanceStop;
    private Vector2 pPosition;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pPosition = player.transform.position;
        step = speed * Time.deltaTime;
        if (type == 0)
        {
            MeleeSeek();
        }
        else if(type == 1)
        {
            RangedSeek();
        }
        else if(type == 2)
        {
            ExplosiveSeek();
            
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
