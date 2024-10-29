using UnityEngine;

// Wrapper for Animator Controller, just calls original functions
// ai-gen start (GPT-4o mini, 2)
public class AnimatorWrapper : IAnimator
{
    Animator animator;

    public AnimatorWrapper(Animator animator)
    {
        this.animator = animator;
    }
    public bool CompareAnimatorStateName(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public bool GetBool(string boolName)
    {
        return animator.GetBool(boolName);
    }

    public void SetBool(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }
}
// ai-gen end