using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcController : MonoBehaviour
{
    public Rigidbody2D pcRigidbody;
    public Collider2D meleeRange;
    public Collider2D pcCollider;

    [Header("Kick Variables")]
    public float kickForce;
    public float kickDelay;
    Vector2 kickForceVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(KickUpdate(kickDelay));
    }
    void ConvertKickForce(float force, float xRotation, float yRotation)
    {
        kickForceVector = new Vector2(force * xRotation, force * yRotation);
    }
    void LaunchKick()
    {
        ConvertKickForce(kickForce, pcRigidbody.transform.rotation.eulerAngles.x, pcRigidbody.transform.rotation.eulerAngles.y);
        pcRigidbody.AddForce(kickForceVector, ForceMode2D.Impulse);
    }
    IEnumerator KickUpdate(float delay)
    {
        yield return new WaitForSeconds(delay);
        LaunchKick();
    }
}
