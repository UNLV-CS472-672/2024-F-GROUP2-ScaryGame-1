using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{
    public GameObject MapBackground;
    public GameObject Map;
    public GameObject PlayerUIDot;
    public Transform PlayerTransform;

    public GameObject AntagonistUIDot;
    public Transform AntagonistTransform;
    public GameObject MinigameUIElement;

    private RectTransform rt_player, rt_antagonist;

    private float map_bounds_x1 = -15.6f;
    private float map_bounds_x2 = 14.3f;
    private float map_bounds_z1 = -6.2f;
    private float map_bounds_z2 = 23.5f;
    private static bool init_minigame = true;
    private int current_minigames_completed = 0;
    private List<GameObject> minigame_sprite_obj = new List<GameObject>();

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
        //UpdateMinigameCompletionStatus(PlayerTransform.position);

        // Check if the M key is pressed
        if (Input.GetKey(KeyCode.M))
        {
            InitializeMinigameUIElements();

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
    private void UpdateMinimapDotPosition(RectTransform RT_Dot, Vector3 WorldPosition)
    {
        RT_Dot.localPosition = new Vector3(((((WorldPosition.x + Mathf.Abs(map_bounds_x1)) / (Mathf.Abs(map_bounds_x1) + Mathf.Abs(map_bounds_x2))) * 340f) - 170f), 0f, 0f);
        RT_Dot.localPosition = new Vector3(RT_Dot.localPosition.x, ((((WorldPosition.z + Mathf.Abs(map_bounds_z1)) / (Mathf.Abs(map_bounds_z1) + Mathf.Abs(map_bounds_z2))) * 340f) - 170f), 0f);
    }

    // Set the active status of the UI Element GameObjects, so that it can be toggled with a keypress in Update()
    private void UpdateGameobjectActive(bool active)
    {
        MapBackground.SetActive(active);
        Map.SetActive(active);
        PlayerUIDot.SetActive(active);
        AntagonistUIDot.SetActive(active);
        foreach(GameObject obj in minigame_sprite_obj)
        {
            obj.SetActive(active);
        }
    }

    private void InitializeMinigameUIElements()
    {
        if (init_minigame)
        {
            init_minigame = false;

            foreach(Vector3 minigame_pos in MinigameManager.miniGamePositions)
            {
                GameObject minigame_ui_element_instance = Instantiate(MinigameUIElement, this.transform);
                minigame_ui_element_instance.SetActive(true);
                UpdateMinimapDotPosition(minigame_ui_element_instance.GetComponent<RectTransform>(), minigame_pos);
                minigame_sprite_obj.Add(minigame_ui_element_instance);
            }
        }
    }

    private void UpdateMinigameCompletionStatus(Vector3 player_position)
    {
        if (MinigameManager.instance.completedMiniGames > current_minigames_completed)
        {
            current_minigames_completed++;

            int closest_minigame_idx = 0;
            
            for (int i = 1; i < minigame_sprite_obj.Count; i++)
            {
                if (Vector3.Distance(player_position, MinigameManager.miniGamePositions[closest_minigame_idx]) < Vector3.Distance(player_position, MinigameManager.miniGamePositions[i]))
                {
                    closest_minigame_idx = i;
                }
                minigame_sprite_obj[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Minimap/complete");
            }

        }
        if (MinigameManager.instance.completedMiniGames < current_minigames_completed)
        {
            current_minigames_completed = 0;
            MinigameManager.miniGamePositions.Clear();
        }
    }
}
