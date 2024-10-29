using UnityEngine;

public interface IAnimator
{
    bool CompareAnimatorStateName(string stateName);
    bool GetBool(string boolName);
    void SetBool(string boolName, bool value);
}

