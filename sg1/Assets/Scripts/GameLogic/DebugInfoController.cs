using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfoController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Canvas canvas;
    public Text text;
    private int frameCount = 0;
    private float timer = 0f;
    void Start()
    {
        canvas = GetComponent<Canvas>();
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
            canvas.enabled = !canvas.enabled;
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
