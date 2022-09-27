using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public bool jumpInput;
    public Vector2 mousePosition;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        jumpInput = false;
        //mousePosition = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
