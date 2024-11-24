using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class RotateDialsMinigame : MonoBehaviour
{
    DialController[] dials;
    public Button stopButton;
    public GameObject gameCanvas;
    public GameObject failCanvas;
    public GameObject successCanvas;
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
        gameCanvas.SetActive(false);
        failCanvas.SetActive(true);
        successCanvas.SetActive(false);

        yield return new WaitForSeconds(2f);
        gameCanvas.SetActive(true);
        failCanvas.SetActive(false);
        successCanvas.SetActive(false);
        gameInProgress = true;
    }

    public IEnumerator WinGame()
    {
        yield return new WaitForSeconds(0.5f);
        gameCanvas.SetActive(false);
        failCanvas.SetActive(false);
        successCanvas.SetActive(true);
        gameInProgress = false;
    }

    bool StopDials()
    {
        foreach(DialController dial in dials)
        {
            if (!dial.InRange()) return false;
        }
        return true;
    }
}
