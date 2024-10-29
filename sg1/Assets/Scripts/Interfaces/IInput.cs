using UnityEngine;

public interface IInput
{
    bool GetMouseButtonDown(int button);
}

public class InputWrapper : IInput
{
    public bool GetMouseButtonDown(int button)
    {
        return Input.GetMouseButtonDown(button);
    }
}