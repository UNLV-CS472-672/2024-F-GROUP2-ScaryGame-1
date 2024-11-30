using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;
public class NewAntagonistControllerTest
{
    private TestSceneHelper helper;

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

    [UnityTest, Order(2)]
    public IEnumerator Update_MovesToPlayer()
    {
        helper.ghostGameObj.SetActive(true);
        yield return null;
        helper.ghostGameObj.transform.position =
            new Vector3(helper.playerGameObj.transform.position.x + 1,
                        helper.ghostGameObj.transform.position.y,
                        helper.playerGameObj.transform.position.z + 1);

        Vector3 ghostPos = helper.ghostGameObj.transform.position;
        Vector3 playerPos = helper.playerGameObj.transform.position;
        ghostPos.y = 0f;
        playerPos.y = 0f;
        float oldDistance = Vector3.Distance(ghostPos, playerPos);

        yield return new WaitForSeconds(0.1f);

        ghostPos = helper.ghostGameObj.transform.position;
        playerPos = helper.playerGameObj.transform.position;
        ghostPos.y = 0f;
        playerPos.y = 0f;
        float newDistance = Vector3.Distance(ghostPos, playerPos);

        Assert.IsTrue(newDistance < oldDistance);
    }

    [UnityTest]
    public IEnumerator Glitch_ChangesPosition()
    {
        helper.ghostGameObj.SetActive(true);
        NewAntagonistController controller = helper.ghostGameObj.GetComponent<NewAntagonistController>();
        Coroutine coroutine = controller.StartCoroutine(controller.Glitch());
        Vector3 oldPos = helper.ghostGameObj.transform.position;

        yield return null;

        Vector3 newPos = helper.ghostGameObj.transform.position;
        yield return new WaitForSeconds(1f);
        controller.StopCoroutine(coroutine);

        Assert.IsTrue(!oldPos.Equals(newPos));
    }

    [UnityTest]
    public IEnumerator Avoid_ChangesPosition()
    {
        helper.ghostGameObj.SetActive(true);
        NewAntagonistController controller = helper.ghostGameObj.GetComponent<NewAntagonistController>();
        Coroutine coroutine = controller.StartCoroutine(controller.Avoid());
        Vector3 oldPos = helper.ghostGameObj.transform.position;

        yield return null;

        Vector3 newPos = helper.ghostGameObj.transform.position;

        yield return new WaitForSeconds(1f);
        controller.StopCoroutine(coroutine);

        Assert.IsTrue(!oldPos.Equals(newPos));
    }
}
