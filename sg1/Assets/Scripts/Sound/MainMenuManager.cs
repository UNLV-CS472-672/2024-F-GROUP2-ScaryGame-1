using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        // Play the main menu music when the scene loads
        SoundManager.Instance?.PlayMainMenuMusic();
    }
}
