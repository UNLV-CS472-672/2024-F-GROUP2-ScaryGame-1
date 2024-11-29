using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player
{
    [OneTimeSetUp]
    public void InitPlayerTests()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnityTest]
    public IEnumerator OneFrameTest()
    {
        yield return new WaitForSeconds(1f);
    }
}
