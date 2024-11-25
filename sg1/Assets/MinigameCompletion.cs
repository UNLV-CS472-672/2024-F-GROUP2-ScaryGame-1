using TMPro;
using UnityEngine;

public class MinigameCompletion : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int completedGames = 0;
    private int totalGames = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        totalGames = MinigameManager.instance.miniGameObjects.Count;
        completedGames = MinigameManager.instance.completedMiniGames;
        text.text = completedGames + "/" + totalGames;
    }

    // Update is called once per frame
    void Update()
    {
        // if number of games completed has change, redisplay text
        if(completedGames != MinigameManager.instance.completedMiniGames)
        {
            completedGames = MinigameManager.instance.completedMiniGames;
            text.text = completedGames + "/" + totalGames;
        }
    }
}
