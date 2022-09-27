using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
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
            speed = 1;
        }
        else if(type == 1)
        {
            RangedSeek();
            speed = 0.9f;
        }
        else if(type == 2)
        {
            ExplosiveSeek();
        }
    }

    void ExplosiveSeek()
    {
        
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
