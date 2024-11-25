using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HelpInfo : MonoBehaviour
{
    //public static HelpInfo instance;
    private string helpMessage;
    private CanvasGroup canvasGroup;
    private TextMeshProUGUI helpText;
    private float timer = 0f;
    private float timeLimit = 0f;

    /*void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        helpText = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Debug.Log("timer: " + timer + ", timeLimit: " +  timeLimit);
        if (timer > timeLimit) 
        {
            //Debug.Log("hello");
            canvasGroup.alpha = 0f;
            timer = 0f;
            timeLimit = 0f;
        }

    }

    public void ShowMessage(string message, float time)
    {
        //Debug.Log("message: " + message);
        if (message != null && helpText.text != message)
        {
            helpText.text = message;
        }
        if(canvasGroup.alpha != 1f)
        {
            canvasGroup.alpha = 1f;
        }
        timer = 0f;
        timeLimit = time;
    }
}
