using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void PlayGame()
    {
        SoundManager.Instance?.StopMusic();
        SceneManager.LoadScene("SampleScene");
    }
}
