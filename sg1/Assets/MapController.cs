using UnityEngine;

public class MapController : MonoBehaviour
{

    private RectTransform rect_transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect_transform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
   {
       Rect rect = new Rect(10, 10, 150, 100);
       UnityEditor.Handles.BeginGUI();
       UnityEditor.Handles.DrawSolidRectangleWithOutline(rect, Color.red, Color.white);
       UnityEditor.Handles.EndGUI();
   }
}
