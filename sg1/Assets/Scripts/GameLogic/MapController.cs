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
        rt_player = PlayerUIDot.GetComponent<RectTransform>();
        rt_antagonist = AntagonistUIDot.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            UpdateGameobjectActive(true);
            UpdateMinimapDotPosition(rt_player, PlayerTransform.transform.position);
            UpdateMinimapDotPosition(rt_antagonist, AntagonistTransform.transform.position);
        }
        else
        {
            UpdateGameobjectActive(false);
        }

    }

    void UpdateMinimapDotPosition(RectTransform RT_Dot, Vector3 WorldPosition)
    {
        RT_Dot.localPosition = new Vector3(((((WorldPosition.x + Mathf.Abs(map_bounds_x1)) / (Mathf.Abs(map_bounds_x1) + Mathf.Abs(map_bounds_x2))) * 340f) - 170f), 0f, 0f);
        RT_Dot.localPosition = new Vector3(RT_Dot.localPosition.x, ((((WorldPosition.z + Mathf.Abs(map_bounds_z1)) / (Mathf.Abs(map_bounds_z1) + Mathf.Abs(map_bounds_z2))) * 340f) - 170f), 0f);
    }

    void UpdateGameobjectActive(bool active)
    {
        MapBackground.SetActive(active);
        Map.SetActive(active);
        PlayerUIDot.SetActive(active);
        AntagonistUIDot.SetActive(active);
    }
}
