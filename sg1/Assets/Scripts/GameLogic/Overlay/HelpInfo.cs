using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Codice.Client.Common;
public class HelpInfo : MonoBehaviour
{
    //public static HelpInfo instance;
    private string helpMessage;
    private CanvasGroup canvasGroup; // used to control visibility
    private TextMeshProUGUI helpText;
    private float timer = 0f;
    private float timeLimit = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        helpText = GetComponentInChildren<TextMeshProUGUI>();
        // disabled by default
        canvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        // disable message once timeLimit has been reached
        if (timer > timeLimit) 
        {
            canvasGroup.alpha = 0f;
            timer = 0f;
            timeLimit = 0f;
        }

    }

    // shows `message` for `time` seconds
    public void ShowMessage(string message, float time)
    {
        // set message text
        if (message != null && helpText.text != message)
        {
            helpText.text = message;
        }

        // make sure it is visible
        if(canvasGroup.alpha != 1f)
        {
            canvasGroup.alpha = 1f;
        }

        // reset timer and set timeLimit
        timer = 0f;
        timeLimit = time;
    }
}
