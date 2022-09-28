using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Ranged_Projectile : Projectile
{
    
    protected override void Awake()
    {
        
    }
    
    protected override void Start()
    {
        
        base.Awake();
        base.Start();
        BulletKick();
        
    }
    
    
    void Update()
    {
        
    }
}
