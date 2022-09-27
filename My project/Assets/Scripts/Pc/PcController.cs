using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcController : MonoBehaviour
{
    public static PcController instance;
    public Rigidbody2D pcRigidbody;
    public Collider2D meleeRange;
    public Collider2D pcCollider;
    bool kickCooldown;
    public Camera mainCamera;

    #region Inputs
    Vector2 mousePosition;
    bool kickInput;
    #endregion Inputs

    [Header("Kick Variables")]
    public float kickForce;     // How Hard The PC Should Kick
    public float kickDelay;     // How Lomg Before Each Kick
    Vector2 kickForceVector;
    public float angle;
    private void Awake()
    {
        #region Simpleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion Simpleton
    }
    void Start()
    {
        GetInputs(true);        //Refreshes Inputs
    }

    void Update()
    {
        GetInputs();
        CharacterAim(mousePosition);
        if (kickInput && !kickCooldown)
        {
            StartCoroutine(KickUpdate(kickDelay));
        }
        //Debug.Log(mousePosition);
    }
    void ConvertKickForce(float force, float xRotation, float yRotation)
    {
        kickForceVector = new Vector2(0, force);
    }
    void LaunchKick()
    {
        ConvertKickForce(kickForce, pcRigidbody.transform.rotation.eulerAngles.x, pcRigidbody.transform.rotation.eulerAngles.y);
        pcRigidbody.AddForce(-transform.up * kickForce, ForceMode2D.Impulse);
    }
    IEnumerator KickUpdate(float delay)     // Coroutine To Be Called In Update, A Timer For Kick
    {
        kickCooldown = true;
        LaunchKick();
        yield return new WaitForSeconds(delay);
        //Debug.Log("I Have Kicked " + kickCooldown);
        kickCooldown = false;
    }
    void GetInputs()                // Used To Get Inputs From Input Maager, Should Be Called On Update
    {
        kickInput = InputManager.instance.kickInput;
        kickInput = InputManager.instance.kickInput;
        mousePosition = InputManager.instance.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    void GetInputs(bool input)      // Use This Overload To Refresh Inputs On Start
    {
        if (input)
        {
            kickInput = false;
            mousePosition = Vector2.zero;
        }
    }
    void CharacterAim(Vector2 mousePosition)        //Gets The Mouse Position And Allows The Player's Y Axis To Always Point At It
    {
        
        angle =(Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x));
        Debug.Log(mousePosition.y + " is Y Position Mouse. " + mousePosition.x + "is X Position Mouse");
        Debug.Log(mousePosition);
        //Debug.Log((Mathf.Rad2Deg*angle)-90);
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * angle) +90);
        //Debug.Log(new Vector2(transform.position.x, transform.position.y) + mousePosition);
        //transform.rotation = eulerAngles(0,0,)
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, mousePosition);
        
    }
}
