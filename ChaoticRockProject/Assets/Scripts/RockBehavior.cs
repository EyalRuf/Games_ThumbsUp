using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    public int score;
    public float stunVelocity;

    [SerializeField]
    private float currentVelocity;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        currentVelocity = rb.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && rb.velocity.magnitude > stunVelocity)
        {
            collision.gameObject.GetComponent<PlayerController>().Stun((collision.transform.position - transform.position).normalized);
        }
    }
}
