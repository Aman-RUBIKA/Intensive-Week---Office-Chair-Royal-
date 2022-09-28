using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midbossAI : MonoBehaviour
{
    public bool isCharging;
    private LineRenderer lr;
    public Vector2 pPosition;
    public float speed = 1;
    public float chargeSpeed;
    public float step ;
    public Vector2 targetPos;
    public float chargeTimer;
    public float chargeMaxTimer;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        chargeSpeed = speed * 2;
        step = speed * Time.deltaTime;
    }

    void Update()
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
                Vector2.MoveTowards(transform.position, new Vector2(targetPos.x * 2,  + targetPos.y * 2), chargeSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, pPosition) <= 15)
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
                lr.SetPosition(1,transform.position);

            }
        }
    }
    void TurnToPlayer()
    {
        float angle;
        angle =(Mathf.Atan2(transform.position.y - pPosition.y, transform.position.x - pPosition.x));
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * angle) - 90);
    }
}
