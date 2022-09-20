using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        base.Subscribe(GetName(), maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Subscribe(string name, float maxHealth)
    {
        base.Subscribe(name, maxHealth);
    }


    public override string GetName()
    {
        return "The Cube";
    }
}
