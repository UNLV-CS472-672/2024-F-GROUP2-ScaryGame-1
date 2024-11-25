using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class RotateDialsMinigame : MonoBehaviour, IMiniGame
{
    DialController[] dials;
    public Button stopButton;
    public GameObject gameCanvas; // canvas with game
    public GameObject failCanvas; // fail message
    public GameObject successCanvas; // win message
    private bool gameInProgress = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dials = GetComponentsInChildren<DialController>();
        stopButton.onClick.AddListener(() => OnButtonClick());
    }

    // Update is called once per frame
    void Update()
    {
        if(gameInProgress) RotateDials();

    }

    // Rotate all dials
    void RotateDials()
    {
        foreach (DialController dial in dials)
        {
            dial.Rotate();
        }
    }

    void OnButtonClick()
    {
        if(StopDials())
        {
            StartCoroutine(WinGame());
        } else
        {
            StartCoroutine(FailGame());
        }
    }
    
    public IEnumerator FailGame()
    {
        gameInProgress = false;
        yield return new WaitForSeconds(0.5f);

        // display fail message for 1 second
        gameCanvas.SetActive(false);
        failCanvas.SetActive(true);
        successCanvas.SetActive(false);
        yield return new WaitForSeconds(1f);

        // start new game
        gameCanvas.SetActive(true);
        failCanvas.SetActive(false);
        successCanvas.SetActive(false);
        gameInProgress = true;
    }

    // Enable canvas with win message
    public IEnumerator WinGame()
    {
        gameInProgress = false;
        yield return new WaitForSeconds(0.5f);
        gameCanvas.SetActive(false);
        failCanvas.SetActive(false);
        successCanvas.SetActive(true);
        CompleteMiniGame();
    }

    // Returns true if all dials are in correct rangel
    bool StopDials()
    {
        foreach(DialController dial in dials)
        {
            if (!dial.InRange()) return false;
        }
        return true;
    }

    public void CompleteMiniGame()
    {
        MinigameManager.instance.MiniGameCompleted();
    }
}
