using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;


public class PortalTests
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
        helper.portalGameObj.SetActive(true);
        yield return null;
    }
    [UnityTest, Order(2)]
    public IEnumerator OnTriggerCollider_LoadsEndScene()
    {
        Portal portal = helper.portalGameObj.GetComponent<Portal>();
        // Assert.That(portal, Is.Not.Null);
        // Assert.That(helper.playerGameObj, Is.Not.Null);
        // Assert.IsTrue(helper.playerGameObj.activeSelf);
        CapsuleCollider collider = helper.playerGameObj.GetComponent<CapsuleCollider>();
        // Assert.That(collider, Is.Not.Null);
        portal.OnTriggerEnter(collider);
        yield return null;
        string sceneName = SceneManager.GetActiveScene().name;
        Assert.IsTrue(sceneName == "EndCredits");
        yield return new WaitForSeconds(25f);
        sceneName = SceneManager.GetActiveScene().name;
        Assert.IsTrue(sceneName == "TitleScreen");
    }

}
