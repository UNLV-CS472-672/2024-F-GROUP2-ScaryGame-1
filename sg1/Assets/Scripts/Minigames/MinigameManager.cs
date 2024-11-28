using UnityEngine;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;

    public GameObject finalObjectToOpen; // The final end goal that will be activated when all minigames have been completed
    public List<GameObject> miniGameObjects; // List of GameObjects with mini-game components
    private List<IMiniGame> miniGames = new List<IMiniGame>();

    public static List<Vector3> miniGamePositions = new List<Vector3>();

    public int completedMiniGames = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        foreach (var gameObject in miniGameObjects)
        {
            IMiniGame miniGame = gameObject.GetComponent<IMiniGame>();
            if (miniGame != null)
            {
                miniGames.Add(miniGame);
            }
            else
            {
                Debug.LogError("One of the listed mini-game objects does not implement IMiniGame interface.");
            }
        }

        if (miniGames.Count == 0)
        {
            Debug.LogError("No mini-games added or mini-games do not implement IMiniGame.");
        }
    }

    public void MiniGameCompleted()
    {
        completedMiniGames++;
        if (completedMiniGames >= miniGames.Count)
        {
            OpenFinalObject();
        }
    }

    private void OpenFinalObject()
    {
        if (finalObjectToOpen == null) return;
        finalObjectToOpen.SetActive(true);
        Debug.Log("All mini-games completed! Final object opened.");
    }
}