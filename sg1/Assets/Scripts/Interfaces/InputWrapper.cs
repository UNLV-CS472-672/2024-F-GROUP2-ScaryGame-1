using UnityEngine;

public class InputWrapper : IInput
{
    public float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName);
    }

    public bool GetKey(KeyCode keyCode)
    {
        return Input.GetKey(keyCode);
    }

    public bool GetKeyDown(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }

    public bool GetMouseButtonDown(int button)
    {
        return Input.GetMouseButtonDown(button);
    }

}