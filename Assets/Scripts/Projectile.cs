using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Enemy") /*&& this.gameObject.CompareTag("Player")*/)
        {
            Debug.Log("Enemy touched");
            other.GetComponent<Enemy>().SubscribeDamage(damage);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Player") /*&& this.gameObject.CompareTag("Enemy")*/)
        {
            Debug.Log("Player touched");
            other.GetComponentInParent<Vehicule>().SubscribeDamage(damage);
            Destroy(this.gameObject);
        }


    }
}
