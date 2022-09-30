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

    [SerializeField] private AudioClip pistolShot;
    [SerializeField] private AudioClip shotgunShot;
    [SerializeField] private AudioClip machineGunShot;


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
            Instantiate(pistolP, new Vector3(forwardT.position.x - offset.x, forwardT.position.y - offset.y), transform.localRotation);
            Instantiate(pistolP, new Vector3(forwardT.position.x + offset.x * 2, forwardT.position.y + offset.y * 2), transform.localRotation);
            Instantiate(pistolP, new Vector3(forwardT.position.x - offset.x * 2, forwardT.position.y - offset.y * 2), transform.localRotation);
            yield return new WaitForSeconds(0.3f);
            pistolCycle = false;

        }
        else if (pistol1)   // If The Player Has 1 Upgrade
        {
            offset = new Vector2(0.25f, 0.25f);
            Instantiate(pistolP, new Vector3(forwardT.position.x + offset.x, forwardT.position.y + offset.y), transform.localRotation);
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

        GameObject inst;
        offset = new Vector2(0.1f, 0.1f);

        if (shotgun2)    // If The Player Has Bought 2 Upgrades
        {
            for (int i = 0; i <= shotgunMagSize*2; i++)
            {
                shotgunSpread(shotgunP, 40, 120, 6, new Vector2(0.25f,0.25f), true, 26);
                shotgunSpread(shotgunP, 40, 120, 6, new Vector2(-0.25f,-0.25f), true, 26);
                yield return new WaitForSeconds(0.6f);
            }
            yield return new WaitForSeconds(shotgunCooldown2);

        }
        else if (shotgun1)   // If The Player Has 1 Upgrade
        {
            for (int i = 0; i <= shotgunMagSize*2; i++)
            {
                shotgunSpread(shotgunP, 40, 120, 5, new Vector2(0.25f,0.25f), true, 25);
                shotgunSpread(shotgunP, 40, 120, 5, new Vector2(-0.25f,-0.25f), true, 25);
                yield return new WaitForSeconds(0.7f);
            }
            yield return new WaitForSeconds(shotgunCooldown1);
        }
        else                // If The Player Owns The Base Item
        {
            for (int i = 0; i <= shotgunMagSize; i++)
            {
                shotgunSpread(shotgunP, 40, 120, 5, new Vector2(0,0), false, 0);
                yield return new WaitForSeconds(0.8f);
            }
            yield return new WaitForSeconds(shotgunCooldown0);
        }
        shotgunCycle = false;
    }
    Vector2 CalculateVectorOffset(Vector2 pos1, Vector2 offset, bool add)    // The Bool Will Determine Whether This Function Adds Or Subtracts The Offset
    {
        if (add)
        {
            return new Vector2(pos1.x + offset.x, pos1.y + offset.y);
        }
        else
        {
            return new Vector2(pos1.x - offset.x, pos1.y - offset.y);
        }
    }

    void shotgunSpread(GameObject prefab, float minAngle, float maxAngle, int bulletNb, Vector2 offset, bool canFreeze, int freezeChance)
    {
        float angleIncrement = (maxAngle - minAngle) / bulletNb;
        GameObject inst;
        for (int i = 0; i < bulletNb; i++)
        {
            inst = Instantiate(prefab, new Vector3(transform.localPosition.x + offset.x, transform.localPosition.y + offset.x, transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z + minAngle + (i* angleIncrement) -  70));
            if (canFreeze)
            {
                float bruh = freezeChance / 100;
                float rng = Random.value;
                if (rng <= bruh)
                {
                    inst.GetComponent<ShotgunWeapon>().canFreeze = true;
                }
            }
        }
    }
}
