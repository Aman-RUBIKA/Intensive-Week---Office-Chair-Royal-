using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAI : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SeekPlayer();
    }

    void SeekPlayer()
    {
        Vector2 pPosition = player.transform.position;
        float angle;
        angle =(Mathf.Atan2(transform.position.y - pPosition.y, transform.position.x - pPosition.x));
        transform.rotation = new Quaternion(0,0,angle,0);
    }
    
    
    
}
