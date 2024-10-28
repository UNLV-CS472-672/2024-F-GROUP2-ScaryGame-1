using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public interface IAnimator
{
    bool CompareAnimatorStateName(string stateName);
    bool GetBool(string boolName);
    void SetBool(string boolName, bool value);
}

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