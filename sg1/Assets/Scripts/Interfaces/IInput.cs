using UnityEngine;

// Interface for Input
public interface IInput
{
    bool GetMouseButtonDown(int button);
    float GetAxis(string axisName);
    bool GetKey(KeyCode keyCode);
    bool GetKeyDown(KeyCode keyCode);
    // We can add more methods as needed for testing
}
