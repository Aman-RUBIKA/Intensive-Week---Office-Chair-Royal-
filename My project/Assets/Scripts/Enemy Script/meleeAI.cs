using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAI : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
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
        SeekPlayer();
    }

    void SeekPlayer()
    {
        float angle;
        float step = speed * Time.deltaTime;
        angle =(Mathf.Atan2(transform.position.y - pPosition.y, transform.position.x - pPosition.x));
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * angle) - 90);
        // turn towards player
        transform.position = Vector2.MoveTowards(transform.position, pPosition, step);
    }
    
}
