using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject MapBackground;
    public GameObject Map;
    public GameObject PlayerUIDot;
    public Transform PlayerTransform;

    public GameObject AntagonistUIDot;
    public Transform AntagonistTransform;

    public GameObject Minigame1;
    public GameObject Minigame2;
    public GameObject Minigame3;
    public GameObject Minigame4;
    public GameObject Minigame5;

    private RectTransform rt_player, rt_antagonist;

    private float map_bounds_z1 = -6.2f;
    private float map_bounds_z2 = 23.5f;
    private float map_bounds_x1 = -15.6f;
    private float map_bounds_x2 = 14.3f;
    private float map_bound_z_mean = 0f;
    private float map_bound_x_mean = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rt_player = PlayerUIDot.GetComponent<RectTransform>();
        rt_antagonist = AntagonistUIDot.GetComponent<RectTransform>();

        

        //map_bound_x_mean = (map_bounds_x1 + map_bounds_x2 / 2f);
        //map_bound_z_mean = (map_bounds_z1 + map_bounds_z2 / 2f);

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (PlayerTransform.position.x > map_bound_x_mean)
        {
            rt_player.localPosition = new Vector3(((PlayerTransform.position.x / map_bounds_x2) * 170f), 0f, 0f);
        }
        else
        {
            rt_player.localPosition = new Vector3(((PlayerTransform.position.x / map_bounds_x1) * -170f), 0f, 0f);
        }
        */
        UpdateMinimapDotPosition(rt_player, PlayerTransform.transform.position);
        UpdateMinimapDotPosition(rt_antagonist, AntagonistTransform.transform.position);

    }

    void UpdateMinimapDotPosition(RectTransform RT_Dot, Vector3 WorldPosition)
    {
        RT_Dot.localPosition = new Vector3(((((WorldPosition.x + Mathf.Abs(map_bounds_x1)) / (Mathf.Abs(map_bounds_x1) + Mathf.Abs(map_bounds_x2))) * 340f) - 170f), 0f, 0f);
        RT_Dot.localPosition = new Vector3(RT_Dot.localPosition.x, ((((WorldPosition.z + Mathf.Abs(map_bounds_z1)) / (Mathf.Abs(map_bounds_z1) + Mathf.Abs(map_bounds_z2))) * 340f) - 170f), 0f);
    }
}
