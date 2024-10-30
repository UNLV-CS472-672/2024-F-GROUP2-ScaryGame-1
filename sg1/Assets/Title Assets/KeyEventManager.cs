using System;
using UnityEngine;

public class KeyEventManager : MonoBehaviour
{
    public static event Action OnEscapeKeyPressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapeKeyPressed?.Invoke();
        }
    }
}
