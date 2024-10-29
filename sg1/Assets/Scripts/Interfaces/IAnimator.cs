using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public interface IAnimator
{
    bool CompareAnimatorStateName(string stateName);
    bool GetBool(string boolName);
    void SetBool(string boolName, bool value);
}

