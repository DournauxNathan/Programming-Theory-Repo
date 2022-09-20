using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE - Child class
public class StandardTurret : Turret
{
    [SerializeField] private int cost = 10;

    [SerializeField] private float multiplier = 3.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot)
        {
            Shoot(multiplier);
        }
    }

    // POLYMORPHISM
    //Override all the UI function to give a new name and display what it is currently transporting
    public override string GetName()
    {
        return "Standard Turret";
    }

    public override string GetData()
    {
        return $"Cost {cost}";
    }
}
