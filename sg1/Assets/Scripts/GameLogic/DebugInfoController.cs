using System.Timers;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfoController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // public Canvas canvas;
    public Text text;
    private CanvasGroup canvasGroup;
    private bool showFPS;
    private int frameCount = 0;
    private float timer = 0f;
    void Start()
    {
        // canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        text = GetComponent<Text>();
        InvokeRepeating("RefreshFPS", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        timer += Time.unscaledDeltaTime;
        if (Input.GetKeyDown(KeyCode.F3))
        {
            // canvasGroup.enabled = !canvasGroup.enabled; 
            canvasGroup.alpha = (canvasGroup.alpha == 1f) ? 0f : 1f;
        }
    }
    void RefreshFPS()
    {
        int fps = (int)(frameCount / timer);
        text.text = "FPS: " + fps;
        frameCount = 0;
        timer = 0f;
    }
}
