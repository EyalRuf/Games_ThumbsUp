using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private bool isWalking;
    [SerializeField]
    private bool isCarrying;
    [SerializeField]
    private bool isJumping;

    void Start()
    {
        isWalking = false;
        isCarrying = false;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
