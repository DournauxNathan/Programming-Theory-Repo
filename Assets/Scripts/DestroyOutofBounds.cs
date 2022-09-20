using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutofBounds : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z <= -50)
        {
            Destroy(this.gameObject);
        }
    }
}
