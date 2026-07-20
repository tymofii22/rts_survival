using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerAnimState curAnimState;

    public void SetAnimState(PlayerAnimState animState)
    {
        if (curAnimState != animState)
        {
            curAnimState = animState;
            Debug.Log($"Animation state changed to: {curAnimState}");

            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                animator.ResetTrigger(parameter.name);
            }

            animator.SetTrigger(curAnimState.ToString());
        }
    }
}

public enum PlayerAnimState
{
    Idle = 0,
    Walk = 1,
    Run = 2,
    Jump = 3
}