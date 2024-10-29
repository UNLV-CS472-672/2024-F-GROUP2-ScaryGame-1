using UnityEngine;
using UnityEngine.UI;

public class ResizeImageToCanvas : MonoBehaviour
{
    private RectTransform canvasRectTransform;
    private RectTransform imageRectTransform;

    void Start()
    {
        // Get the RectTransform components
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        imageRectTransform = GetComponent<RectTransform>();

        // Resize the image to match the canvas size
        ResizeImage();
    }

    void ResizeImage()
    {
        // Set the size of the image to match the canvas size
        imageRectTransform.sizeDelta = new Vector2(canvasRectTransform.rect.width, canvasRectTransform.rect.height);
    }
}
