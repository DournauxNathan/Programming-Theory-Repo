using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE - Child class
public class StandardVehicule : Vehicule
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioSource.PlayOneShot(crashEffect, 1.0f);
            explosionEffect.Play();
            SubscribeDamage(maxHealth);
        }
    }

}
