using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;
public class DamagerTest
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
    public IEnumerator Damager_Damages()
    {
        helper.ghostGameObj.SetActive(true);
        yield return null;
        helper.ghostGameObj.transform.position =
            new Vector3(helper.playerGameObj.transform.position.x + 0.5f,
                        helper.ghostGameObj.transform.position.y,
                        helper.playerGameObj.transform.position.z + 0.5f);
        HealthSlider health = helper.healthSliderGameObj.GetComponent<HealthSlider>();
        float oldHealth = health.currentHealth;
        yield return new WaitForSeconds(2f);
        Assert.IsTrue(health.currentHealth < oldHealth);
    }

    [UnityTest, Order(3)]
    public IEnumerator Damager_Die()
    {
        helper.ghostGameObj.SetActive(true);
        yield return null;
        helper.ghostGameObj.transform.position =
            new Vector3(helper.playerGameObj.transform.position.x + 0.5f,
                        helper.ghostGameObj.transform.position.y,
                        helper.playerGameObj.transform.position.z + 0.5f);
        HealthSlider health = helper.healthSliderGameObj.GetComponent<HealthSlider>();
        health.currentHealth = 0f;
        yield return new WaitForSeconds(0.1f);

        string sceneName = SceneManager.GetActiveScene().name;
        Assert.AreEqual(sceneName, "TitleScreen");
    }
}
