using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string endCreditsSceneName = "EndCredits"; // Name of the end credits scene

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the portal is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal. Loading end credits...");
            LoadEndCreditsScene();
        }
    }

    private void LoadEndCreditsScene()
    {
        // Load the specified end credits scene
        SceneManager.LoadScene(endCreditsSceneName);
    }
}
