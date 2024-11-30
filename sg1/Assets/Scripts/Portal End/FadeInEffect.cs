using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInAndScrollCreditsWithMusic : MonoBehaviour
{
    public CanvasGroup backgroundCanvasGroup;  // Background CanvasGroup for fading
    public CanvasGroup textCanvasGroup;        // Text CanvasGroup for fading
    public RectTransform creditsTextTransform; // RectTransform of the main credits text
    public RectTransform additionalTextTransform; // RectTransform of the additional text
    public RectTransform imageTransform;       // RectTransform of the scrolling image
    public float fadeDuration = 2.0f;          // Duration for fade-in effects
    public float scrollDuration = 10.0f;       // Duration for the scrolling effect
    public float scrollDistance = 500.0f;      // How far the text and image should scroll (Y-axis)
    public AudioSource audioSource;            // Reference to the AudioSource for the music
    public string mainMenuSceneName = "TitleScreen"; // Name of the main menu scene

    void Start()
    {
        // Initialize alpha values
        backgroundCanvasGroup.alpha = 0.0f; // Background starts transparent
        textCanvasGroup.alpha = 0.0f;       // Text starts transparent

        // Start the music
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Start the sequence of fading and scrolling
        StartCoroutine(FadeInThenScrollCredits());
    }

    private System.Collections.IEnumerator FadeInThenScrollCredits()
    {
        // Fade in the background
        yield return StartCoroutine(FadeCanvasGroup(backgroundCanvasGroup, 1.0f));

        // Wait a brief moment
        yield return new WaitForSeconds(0.5f);

        // Fade in the text
        yield return StartCoroutine(FadeCanvasGroup(textCanvasGroup, 1.0f));

        // Wait a moment before starting the scroll
        yield return new WaitForSeconds(0.5f);

        // Scroll text and image
        yield return StartCoroutine(ScrollCredits());

        // After scrolling finishes, load the main menu
        ResetToMainMenu();
    }

    private System.Collections.IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha; // Ensure exact target value
    }

    private System.Collections.IEnumerator ScrollCredits()
    {
        Vector2 startPositionCredits = creditsTextTransform.anchoredPosition;
        Vector2 endPositionCredits = startPositionCredits + new Vector2(0, scrollDistance);

        Vector2 startPositionAdditional = additionalTextTransform.anchoredPosition;
        Vector2 endPositionAdditional = startPositionAdditional + new Vector2(0, scrollDistance);

        Vector2 startPositionImage = imageTransform.anchoredPosition;
        Vector2 endPositionImage = startPositionImage + new Vector2(0, scrollDistance);

        float elapsedTime = 0.0f;

        while (elapsedTime < scrollDuration)
        {
            float t = elapsedTime / scrollDuration;

            // Update the positions of the credits text, additional text, and image
            creditsTextTransform.anchoredPosition = Vector2.Lerp(startPositionCredits, endPositionCredits, t);
            additionalTextTransform.anchoredPosition = Vector2.Lerp(startPositionAdditional, endPositionAdditional, t);
            imageTransform.anchoredPosition = Vector2.Lerp(startPositionImage, endPositionImage, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure exact target positions
        creditsTextTransform.anchoredPosition = endPositionCredits;
        additionalTextTransform.anchoredPosition = endPositionAdditional;
        imageTransform.anchoredPosition = endPositionImage;
    }

    private void ResetToMainMenu()
    {
        // Stop the music
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}