using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public bool kickInput;
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
        kickInput = false;
        //mousePosition = 
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        kickInput = Input.GetMouseButtonDown(0) ;
    }
}
