using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;

public class FloorSaltLogicTest
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
        helper.ghostGameObj.SetActive(true);
        yield return null;
    }
    [UnityTest]
    public IEnumerator SaltEnabled_AddsToList()
    {
        helper.floorsaltGameObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        Assert.IsTrue(FloorSaltLogic.saltInstances.Count > 0);
        FloorSaltLogic.saltInstances.Clear();
    }
}

