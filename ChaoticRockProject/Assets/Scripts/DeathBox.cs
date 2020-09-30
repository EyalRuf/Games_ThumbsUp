using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public float deathDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Rock"))
        {
            other.tag = "Dead";
            Destroy(other.gameObject, deathDelay); 
        }
    }
}
