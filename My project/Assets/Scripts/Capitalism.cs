using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capitalism : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    
    void Start()
    {
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Openshop()
    {
        if (Input.GetKey("tab"))
        {
            Menu.SetActive(true);
        }
    }
}
