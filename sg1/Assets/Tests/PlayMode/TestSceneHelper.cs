using UnityEngine;

public class TestSceneHelper : MonoBehaviour
{
    public GameObject playerGameObj; // Inactive
    public GameObject ghostGameObj; // Inactive
    public GameObject overlayGameObj; // Inactive
    public GameObject pauseMenuManagerGameObj; // Inactive
    public GameObject pauseMenuGameObj; // Inactive, only active when paused
    public GameObject optionsCanvasGameObj; // Inactive, only active when paused and in options
    public GameObject sequenceMinigameCanvasGameObj;
    public GameObject rotatingDialsGameObj;
    public GameObject healthSliderGameObj; 

    /*
    public void ActivateAll()
    {
        playerGameObj.SetActive(true);
        ghostGameObj.SetActive(true);
        overlayGameObj.SetActive(true);
    }*/
}
