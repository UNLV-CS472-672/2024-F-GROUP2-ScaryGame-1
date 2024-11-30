using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;
using JetBrains.Annotations;
public class PauseMenuTest
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
        helper.pauseMenuManagerGameObj.SetActive(true);

        yield return null;
    }

    [UnityTest, Order(2)]
    public IEnumerator Start_FindsOptionsMenuTitle()
    {
        OptionsMenuTitle optionsMenuTitle = helper.pauseMenuManagerGameObj.GetComponent<OptionsMenuTitle>();
        yield return null;
        Assert.That(optionsMenuTitle.optionsCanvas, Is.Not.Null);
    }

    [UnityTest, Order(2)]
    public IEnumerator OpenOptions_OpensAndCloses()
    {
        helper.pauseMenuGameObj.SetActive(true);
        PauseMenu menu = helper.pauseMenuManagerGameObj.GetComponent<PauseMenu>();
        menu.optionsButton.onClick.Invoke();
        yield return null;
        Assert.IsTrue(menu.optionsCanvas.activeSelf);
        OptionsMenuTitle optionsMenu = helper.pauseMenuManagerGameObj.GetComponentInChildren<OptionsMenuTitle>();
        optionsMenu.backButtonMainPanel.onClick.Invoke();
        yield return null;
        Assert.IsFalse(menu.optionsCanvas.activeSelf);

        helper.pauseMenuGameObj.SetActive(false);
    }

    [UnityTest, Order(2)]
    public IEnumerator Back_DisablesCanvas()
    {
        helper.pauseMenuGameObj.SetActive(true);
        PauseMenu menu = helper.pauseMenuManagerGameObj.GetComponent<PauseMenu>();
        menu.resumeButton.onClick.Invoke();
        yield return null;

        Assert.IsFalse(helper.pauseMenuGameObj.activeSelf);
    }

    [UnityTest, Order(2)]
    public IEnumerator Back_ReenablesMovement()
    {
        helper.pauseMenuGameObj.SetActive(true);
        PauseMenu menu = helper.pauseMenuManagerGameObj.GetComponent<PauseMenu>();
        menu.resumeButton.onClick.Invoke();
        yield return null;

        Assert.IsFalse(Movement.LockMovement);
    }

    [UnityTest, Order(2)]
    public IEnumerator ShowSensitivity_EnablesSensitivityCanvas()
    {
        helper.optionsCanvasGameObj.SetActive(true);
        OptionsMenuTitle optionsMenu = helper.pauseMenuManagerGameObj.GetComponent<OptionsMenuTitle>();
        optionsMenu.sensitivityButton.onClick.Invoke();
        yield return null;

        Assert.IsTrue(optionsMenu.sensitivityPanel.activeSelf);
        optionsMenu.backButtonSensitivityPanel.onClick.Invoke();
        helper.optionsCanvasGameObj.SetActive(false);
    }

    [UnityTest, Order(2)]
    public IEnumerator ChangeSensitivitySlider()
    {
        helper.optionsCanvasGameObj.SetActive(true);
        OptionsMenuTitle optionsMenu = helper.pauseMenuManagerGameObj.GetComponent<OptionsMenuTitle>();
        optionsMenu.sensitivityButton.onClick.Invoke();
        yield return null;

        optionsMenu.sensitivityXSlider.onValueChanged.Invoke(0.2f);
        optionsMenu.sensitivityXSlider.value = 0.2f;
        yield return null;

        Assert.IsTrue(optionsMenu.sensitivityApplyButton.interactable);

        optionsMenu.sensitivityApplyButton.onClick.Invoke();
        yield return null;

        optionsMenu.sensitivitySetToDefaultButton.onClick.Invoke();
        yield return null;

        optionsMenu.backButtonSensitivityPanel.onClick.Invoke();
        helper.optionsCanvasGameObj.SetActive(false);
    }

    [UnityTest, Order(2)]
    public IEnumerator ChangeSensitivityInput()
    {
        helper.optionsCanvasGameObj.SetActive(true);
        OptionsMenuTitle optionsMenu = helper.pauseMenuManagerGameObj.GetComponent<OptionsMenuTitle>();
        optionsMenu.sensitivityButton.onClick.Invoke();
        yield return null;

        optionsMenu.sensitivityXInputField.onEndEdit.Invoke("0.2");
        yield return null;

        Assert.IsFalse(optionsMenu.sensitivityApplyButton.interactable);

        optionsMenu.backButtonSensitivityPanel.onClick.Invoke();
        helper.optionsCanvasGameObj.SetActive(false);
    }

    [UnityTest, Order(3)]
    public IEnumerator BackToTitle_LoadsTitle()
    {
        helper.pauseMenuGameObj.SetActive(true);
        PauseMenu menu = helper.pauseMenuManagerGameObj.GetComponent<PauseMenu>();
        menu.backToTitleButton.onClick.Invoke();
        yield return null;

        string sceneName = SceneManager.GetActiveScene().name;
        Assert.AreEqual(sceneName, "TitleScreen");
    }
}

