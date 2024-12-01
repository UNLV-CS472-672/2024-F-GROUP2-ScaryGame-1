using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

public class PressTheButtonMinigameTest
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
    public IEnumerator GameEnabled_ShowsCanvas()
    {
        helper.sequenceMinigameCanvasGameObj.SetActive(true);
        yield return new WaitForSeconds(10f);
        PressTheButtonMinigame game = helper.sequenceMinigameCanvasGameObj.GetComponent<PressTheButtonMinigame>();
        Assert.IsTrue(game.miniGameCanvas.activeSelf);

        game.buttons[0].onClick.Invoke();
        yield return null;
        game.buttons[0].onClick.Invoke();
        yield return new WaitForSeconds(2f);
        helper.sequenceMinigameCanvasGameObj.SetActive(false);
        yield return null;
        helper.sequenceMinigameCanvasGameObj.SetActive(true);
        yield return null;
    }
}
