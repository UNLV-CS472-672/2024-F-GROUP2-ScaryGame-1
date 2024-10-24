using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ResizeRawImageToCanvas : MonoBehaviour
{
    private RectTransform canvasRectTransform;
    private RectTransform rawImageRectTransform;
    public GameObject placeholderImage; // Reference to the placeholder image
    public VideoPlayer videoPlayer; // Reference to the Video Player

    void Start()
    {
        // Get the RectTransform components
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        rawImageRectTransform = GetComponent<RectTransform>();

        // Resize the Raw Image to match the canvas size
        ResizeRawImage();

        // Subscribe to the video player's prepare completed event
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    void ResizeRawImage()
    {
        // Set the size of the Raw Image to match the canvas size
        rawImageRectTransform.sizeDelta = new Vector2(canvasRectTransform.rect.width, canvasRectTransform.rect.height);
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // Hide the placeholder image when the video is ready to play
        placeholderImage.SetActive(false);
        videoPlayer.Play();
    }
}
