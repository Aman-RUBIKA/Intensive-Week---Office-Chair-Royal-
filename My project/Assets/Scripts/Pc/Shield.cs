using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    int enemyProjLayer = 9;
    bool shieldUpgrade0, shieldUpgrade1, shieldUpgrade2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool IfShieldUpgrade()
    {
        if (shieldUpgrade0)
        {
            return true;
        }
        else { return false; }
    }
    void HandleProj(Collider2D col)
    {
        if (col.gameObject.layer == enemyProjLayer)
        {
            Vector2 vel;
            vel = col.attachedRigidbody.velocity;
            if (IfShieldUpgrade())
            {
                if (shieldUpgrade2)
                {

                }
                else if (shieldUpgrade1)
                {

                }
                else
                {

                }
            }
            Debug.Log(vel + " is Velocity Of Bullet Before Changing");
            vel = -vel;
            Debug.Log(vel + " is Velocity Of Bullet After Changing");
        }
    }
}
