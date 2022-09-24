using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Enemy
{
    private void OnEnable()
    {
        base.Subscribe(this.gameObject, GetName(), maxHealth);
    }

    private void FixedUpdate()
    {
        GoTo(-Vector3.forward, 1f);
    }

    public override void Subscribe(GameObject go,string name, float maxHealth)
    {
        base.Subscribe(go, name, maxHealth);
    }


    public override string GetName()
    {
        return "The Cube";
    }
}
