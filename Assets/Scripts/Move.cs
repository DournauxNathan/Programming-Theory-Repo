using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (this.CompareTag("Health") && !GameManager.Instance.GetGameOver())
        {
            transform.Translate(-Vector3.forward * 50f * Time.deltaTime);

        }
        else if (!GameManager.Instance.GetGameOver())
        {
            transform.Translate(-Vector3.forward * SpawnManager.Instance.obstacleSpeed * 10f * Time.deltaTime);
        }
    }
}
