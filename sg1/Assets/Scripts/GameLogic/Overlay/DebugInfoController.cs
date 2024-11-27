using UnityEngine;
using UnityEngine.UI;

public class DebugInfoController : MonoBehaviour
{
    public Text text;
    private CanvasGroup canvasGroup;
    private int frameCount = 0;
    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // public Canvas canvas;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponent<Text>();

        // invisible by default
        canvasGroup.alpha = 0f;

        // refresh FPS calculation every second
        InvokeRepeating("RefreshFPS", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;

        // keep track of time manually bc of variability with InvokeRepeating
        timer += Time.unscaledDeltaTime;
        // F3 toggles visibility
        if (Input.GetKeyDown(KeyCode.F3))
        {
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
