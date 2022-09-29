using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    static int enemyProjLayer = 9;
    static int pcProjLayer = 8;
    [SerializeField]
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
            //Vector3 reverse = new Vector3(0, 0, 180);
            //Quaternion temp;
            vel = col.attachedRigidbody.velocity;
            if (IfShieldUpgrade())
            {
                col.gameObject.layer = pcProjLayer;
                col.GetComponent<Enemy_Ranged_Projectile>().isReflected = true;
                if (shieldUpgrade2)
                {

                }
                else if (shieldUpgrade1)
                {

                }
                else
                {
                    //Debug.Log(vel + " is Velocity Of Bullet Before Changing");
                    vel = -vel;
                    col.attachedRigidbody.velocity = vel;
                    //Debug.Log(col.attachedRigidbody.velocity + " is Velocity Of Bullet After Changing and " + -vel+ " is vel");

                }
            }
            else
            {
                Destroy(col.gameObject);
            }
            
            /*temp = col.gameObject.transform.localRotation;
            reverse.z += col.gameObject.transform.localRotation.z  ;
            temp.z = reverse.z;
            col.gameObject.transform.localRotation = temp;*/
        }
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        HandleProj(col);
    }
}
