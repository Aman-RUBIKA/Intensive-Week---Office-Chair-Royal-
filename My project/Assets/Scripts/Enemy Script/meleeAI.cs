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
    void LateUpdate()
    {
        SeekPlayer();
    }

    void SeekPlayer()
    {
        float angle;
        Vector2 playerPosition = player.transform.position;
        angle =(Mathf.Atan2(transform.position.y - playerPosition.y, transform.position.x - playerPosition.x));
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * angle) - 90);
    }
    
    
    
}
