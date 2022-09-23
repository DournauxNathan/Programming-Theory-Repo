using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 50f;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.GetGameOver())
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
    }
}
