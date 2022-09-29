using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    private Vector2 offset;
    public Transform forwardT, rightT, backT, leftT, forwardShotgun;    // Transforms That The Player Fires From
    
    
    public bool pistol0, pistol1, pistol2, pistolCycle;

    public bool mach0, mach1, mach2, machCycle;
    public int machMagSize;
    [SerializeField]
    float machCooldown0, machCooldown1, machCooldown2;

    [SerializeField]
    public bool shotgun0, shotgun1, shotgun2, shotgunCycle;
    public int shotgunMagSize;
    [SerializeField]
    float shotgunCooldown0, shotgunCooldown1, shotgunCooldown2;


    public GameObject pistolP, machP, shotgunP;       // All The Prefabs For Projectiles
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
        if (mach0 && !machCycle)
        {
            StartCoroutine(MachineGunShoot());
        }
        if (shotgun0 && !shotgunCycle)
        {
            StartCoroutine(ShotgunShoot());
        }
    }
    IEnumerator PistolShoot()
    {
        pistolCycle = true;
        if (pistol2)    // If The Player Has Bought 2 Upgrades
        {
            offset = new Vector2(0.25f, 0.25f);
            Instantiate(pistolP, new Vector3(forwardT.position.x + offset.x, forwardT.position.y + offset.y), transform.localRotation);
            yield return new WaitForSeconds(0.2f);
            Instantiate(pistolP, new Vector3(forwardT.position.x - offset.x, forwardT.position.y - offset.y), transform.localRotation);
            yield return new WaitForSeconds(0.2f);
            Instantiate(pistolP, new Vector3(forwardT.position.x + offset.x * 2, forwardT.position.y + offset.y * 2), transform.localRotation);
            yield return new WaitForSeconds(0.7f);
            Instantiate(pistolP, new Vector3(forwardT.position.x - offset.x * 2, forwardT.position.y - offset.y * 2), transform.localRotation);
            yield return new WaitForSeconds(0.7f);
            pistolCycle = false;

        }
        else if (pistol1)   // If The Player Has 1 Upgrade
        {
            offset = new Vector2(0.25f, 0.25f);
            //Instantiate(pistolP, forwardT.position, transform.localRotation);
            Instantiate(pistolP, new Vector3(forwardT.position.x + offset.x, forwardT.position.y + offset.y), transform.localRotation);
            yield return new WaitForSeconds(0.2f);
            //Instantiate(pistolP, forwardT.position, transform.localRotation);
            Instantiate(pistolP, new Vector3(forwardT.position.x - offset.x, forwardT.position.y - offset.y), transform.localRotation);
            yield return new WaitForSeconds(0.35f);
        }
        else                // If The Player Owns The Base Item
        {
            Instantiate(pistolP, forwardT.position, transform.localRotation);
            yield return new WaitForSeconds(0.5f);
        }
        pistolCycle = false;
    }
    IEnumerator MachineGunShoot()
    {
        machCycle = true;
        if (mach2)    // If The Player Has Bought 2 Upgrades
        {
            
            Instantiate(machP, forwardT.position, transform.localRotation);
            yield return new WaitForSeconds(0.1f);


        }
        else if (mach1)   // If The Player Has 1 Upgrade
        {
            Instantiate(machP, forwardT.position, transform.localRotation);
            yield return new WaitForSeconds(0.25f);
        }
        else                // If The Player Owns The Base Item
        {
            for (int i = 0; i <= machMagSize; i++)
            {
                Instantiate(machP, forwardT.position, transform.localRotation);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(machCooldown0);
        }
        machCycle = false;
    }
    IEnumerator ShotgunShoot()
    {
        shotgunCycle = true;
        if (shotgun2)    // If The Player Has Bought 2 Upgrades
        {
            offset.x = 1;
            offset.y = 1;
            Instantiate(shotgunP, new Vector3(forwardShotgun.position.x + offset.x, forwardShotgun.position.y + offset.y), transform.localRotation);
            yield return new WaitForSeconds(0.2f);
            Instantiate(shotgunP, new Vector3(forwardShotgun.position.x - offset.x, forwardShotgun.position.y - offset.y), transform.localRotation);
            yield return new WaitForSeconds(0.2f);


        }
        else if (shotgun1)   // If The Player Has 1 Upgrade
        {
            offset.x = 1;
            offset.y = 1;
            //Instantiate(shotgunP, forwardShotgun.position, transform.localRotation);
            Instantiate(shotgunP, new Vector3(forwardShotgun.position.x + offset.x, forwardShotgun.position.y + offset.y), transform.localRotation);
            yield return new WaitForSeconds(0.2f);
            Instantiate(shotgunP, new Vector3(forwardShotgun.position.x - offset.x, forwardShotgun.position.y - offset.y), transform.localRotation);
            yield return new WaitForSeconds(0.2f);
        }
        else                // If The Player Owns The Base Item
        {
            for (int i = 0; i <= shotgunMagSize; i++)
            {
                Instantiate(shotgunP, forwardShotgun.position , transform.localRotation);
                yield return new WaitForSeconds(0.8f);
            }
            yield return new WaitForSeconds(machCooldown0);
        }
        shotgunCycle = false;
    }

}
