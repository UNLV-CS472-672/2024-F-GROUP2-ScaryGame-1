using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

public class RotateDialsMinigameTest
{
    private TestSceneHelper helper;

    // This is not a real test, I was struggling to make sure the scene was loaded before tests
    // There is probably a better solution, but the Order(1) makes it so this runs first.
    [UnityTest, Order(1)]
    public IEnumerator LoadScene()
    {
        SceneManager.LoadScene("TestScene");
        yield return new WaitForSecondsRealtime(2f);
        var helperObj = GameObject.Find("TestHelper");
        Assert.That(helperObj, Is.Not.Null);
        helper = helperObj.GetComponent<TestSceneHelper>();
        helper.playerGameObj.SetActive(true);
        yield return null;
    }
    [UnityTest]
    public IEnumerator LoadGame_ShowsCanvas()
    {
        helper.rotatingDialsGameObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        RotateDialsMinigame game = helper.rotatingDialsGameObj.GetComponent<RotateDialsMinigame>();
        Assert.IsTrue(game.gameCanvas.activeSelf);
        helper.rotatingDialsGameObj.SetActive(false);
    }

    [UnityTest]
    public IEnumerator PressButton_LoseGame()
    {
        helper.rotatingDialsGameObj.SetActive(true);
        yield return null;
        RotateDialsMinigame game = helper.rotatingDialsGameObj.GetComponent<RotateDialsMinigame>();
        while(game.StopDials())
        {
            yield return null;
        }
        game.stopButton.onClick.Invoke();
        yield return new WaitForSeconds(2f);
        Assert.IsTrue(game.failCanvas.activeSelf || game.gameCanvas.activeSelf);
        helper.rotatingDialsGameObj.SetActive(false);
    }

    [UnityTest]
    public IEnumerator PressButton_WinGame()
    {
        helper.rotatingDialsGameObj.SetActive(true);
        yield return null;
        RotateDialsMinigame game = helper.rotatingDialsGameObj.GetComponent<RotateDialsMinigame>();
        while (!game.StopDials())
        {
            yield return null;
        }
        game.stopButton.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        Assert.IsTrue(game.successCanvas.activeSelf);
    }
}
