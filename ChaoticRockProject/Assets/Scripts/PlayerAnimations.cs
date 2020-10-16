using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    const string ANIM_PARAM_NAME_WALK = "walk";
    const string ANIM_PARAM_NAME_JUMP = "jump";
    const string ANIM_PARAM_NAME_HOLDING = "holding";
    const string ANIM_PARAM_NAME_THROWING = "throw";

    [SerializeField] private Animator animator;
    [SerializeField] public bool isWalking { get; set; }
    
    void Start()
    {
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking != animator.GetBool(ANIM_PARAM_NAME_WALK))
            animator.SetBool(ANIM_PARAM_NAME_WALK, isWalking);
    }

    public void TriggerJumpAnimation ()
    {
        animator.SetTrigger(ANIM_PARAM_NAME_JUMP);
    }

    public void TriggerThrowAnimation()
    {
        animator.SetTrigger(ANIM_PARAM_NAME_THROWING);
        this.ToggleCarryAnimation();
    }
    public void ToggleCarryAnimation()
    {
        animator.SetBool(ANIM_PARAM_NAME_HOLDING, !animator.GetBool(ANIM_PARAM_NAME_HOLDING));
    }
}
