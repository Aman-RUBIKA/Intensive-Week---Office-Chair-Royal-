using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public Transform forwardT, rightT, backT, leftT;
    [SerializeField]
    bool pistol0, pistol1, pistol2, pistolCycle;
    public GameObject pistolP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pistol0 && !pistolCycle)
        {
            StartCoroutine(PistolShoot());
        }
    }
    IEnumerator PistolShoot()
    {
        pistolCycle = true;
        if (pistol2)    // If The Player Has Bought 2 Upgrades
        {
            Instantiate(pistolP, forwardT.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(pistolP, forwardT.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(pistolP, forwardT.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            pistolCycle = false;

        }
        else if (pistol1)   // If The Player Has 1 Upgrade
        {
            Instantiate(pistolP, forwardT.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(pistolP, forwardT.position, Quaternion.identity);
            yield return new WaitForSeconds(0.6f);
        }
        else                // If The Player Owns The Base Item
        {
            Instantiate(pistolP, forwardT.position, transform.localRotation);
            yield return new WaitForSeconds(0.5f);
        }
        pistolCycle = false;
    }
}
