using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;

public class InteractableTest
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
        helper.saltGameObj.SetActive(true);
        yield return null;
    }
    [UnityTest]
    public IEnumerator SaltEnabled_AddsToList()
    {
        Interactable interactable = helper.saltGameObj.GetComponent<Interactable>();
        InventoryManagement manager = helper.overlayGameObj.GetComponentInChildren<InventoryManagement>();

        interactable.Interact();
        yield return null;

        Assert.IsFalse(manager.hotbar[0].empty);
    }
}


