using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject MapBackground;
    public GameObject Map;
    public GameObject PlayerUIDot;
    public Transform PlayerTransform;

    public GameObject AntagonistUIDot;
    public Transform AntagonistTransform;

    private RectTransform rt_player, rt_antagonist;

    private float map_bounds_x1 = -15.6f;
    private float map_bounds_x2 = 14.3f;
    private float map_bounds_z1 = -6.2f;
    private float map_bounds_z2 = 23.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateGameobjectActive(false);
        rt_player = PlayerUIDot.GetComponent<RectTransform>();
        rt_antagonist = AntagonistUIDot.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the M key is pressed
        if (Input.GetKey(KeyCode.M))
        {
            // Enable the Map UI elements, and modify their positions using UpdateMinimapDotPosition()
            UpdateGameobjectActive(true);
            UpdateMinimapDotPosition(rt_player, PlayerTransform.transform.position);
            UpdateMinimapDotPosition(rt_antagonist, AntagonistTransform.transform.position);
        }
        // Otherwise, disable UI GameObjects
        else
        {
            UpdateGameobjectActive(false);
        }

    }

    // Translate the 3D world position of a GameObject into a 2D coordinate that will fit over the Map Foreground image
    void UpdateMinimapDotPosition(RectTransform RT_Dot, Vector3 WorldPosition)
    {
        RT_Dot.localPosition = new Vector3(((((WorldPosition.x + Mathf.Abs(map_bounds_x1)) / (Mathf.Abs(map_bounds_x1) + Mathf.Abs(map_bounds_x2))) * 340f) - 170f), 0f, 0f);
        RT_Dot.localPosition = new Vector3(RT_Dot.localPosition.x, ((((WorldPosition.z + Mathf.Abs(map_bounds_z1)) / (Mathf.Abs(map_bounds_z1) + Mathf.Abs(map_bounds_z2))) * 340f) - 170f), 0f);
    }

    // Set the active status of the UI Element GameObjects, so that it can be toggled with a keypress in Update()
    void UpdateGameobjectActive(bool active)
    {
        MapBackground.SetActive(active);
        Map.SetActive(active);
        PlayerUIDot.SetActive(active);
        AntagonistUIDot.SetActive(active);
    }
}
