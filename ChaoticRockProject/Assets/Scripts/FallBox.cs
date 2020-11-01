using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FallBox : MonoBehaviour
{
    AudioSource auSource;

    void Start()
    {
        auSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            auSource.Play();
        }
    }
}
