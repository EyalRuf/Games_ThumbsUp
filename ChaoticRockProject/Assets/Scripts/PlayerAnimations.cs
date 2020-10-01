using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    const string ANIM_PARAM_NAME_WALK = "walk";
    const string ANIM_PARAM_NAME_JUMP = "jump";
    const string ANIM_PARAM_NAME_CARRY = "carrying";

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
        animator.SetBool(ANIM_PARAM_NAME_WALK, isWalking);
        animator.SetBool(ANIM_PARAM_NAME_JUMP, isJumping);
        animator.SetBool(ANIM_PARAM_NAME_CARRY, isCarrying);
    }
}
