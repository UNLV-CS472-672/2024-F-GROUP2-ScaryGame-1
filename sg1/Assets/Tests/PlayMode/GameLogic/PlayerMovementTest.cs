using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovementTest
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
        helper.overlayGameObj.SetActive(true);
        yield return null;
    }
    [UnityTest]
    public IEnumerator Update_NoMovement()
    {
        Vector3 oldPos = helper.playerGameObj.transform.position;
        yield return null;
        Assert.IsTrue(oldPos.Equals(helper.playerGameObj.transform.position));
    }

    [UnityTest]
    public IEnumerator Update_NoRotation()
    {
        Vector3 oldRot = helper.playerGameObj.transform.localEulerAngles;
        yield return null;
        Assert.IsTrue(oldRot.Equals(helper.playerGameObj.transform.localEulerAngles));

    }
}
