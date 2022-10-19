using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public bool kickInput;
    public Vector2 mousePosition;
    public bool pauseInput;
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
        kickInput = false;
        pauseInput = false;
        //mousePosition = 
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition   =     Input.mousePosition;
        kickInput       =     Input.GetMouseButtonDown(0);
        pauseInput      =     Input.GetKeyDown(KeyCode.Tab);
    }
}
