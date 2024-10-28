using System.Collections;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DoorControllerTest
{
    private DoorController doorController;
    private GameObject gameObject;
    private string currentStateName;
    private bool wantClosed;

    [SetUp]
    public void SetUpDoorController() 
    {
        gameObject = new GameObject();
        gameObject.AddComponent<DoorController>();
        doorController = gameObject.GetComponent<DoorController>();

        currentStateName = "closed";
        wantClosed = true;

        var mockAnimator = new Mock<IAnimator>();
        mockAnimator.Setup(m => m.CompareAnimatorStateName(It.IsAny<string>()))
            .Returns((string s) => s == currentStateName);
        mockAnimator.Setup(m => m.SetBool("isClosed", It.IsAny<bool>()))
            .Callback((string s, bool b) =>  wantClosed = b);
        mockAnimator.Setup(m => m.GetBool("isClosed")).Returns(() => wantClosed);

        doorController.animator = mockAnimator.Object;
    }
    [Test]
    public void toggleDoor_NoChange()
    {
        currentStateName = "closed";
        wantClosed = false;

        doorController.toggleDoor();

        Assert.That(wantClosed, Is.False);
    }

    public void toggleDoor_Change()
    {
        currentStateName = "closed";
        wantClosed = false;

        doorController.toggleDoor();

        Assert.That(wantClosed, Is.False);
    }

    [TearDown]
    public void TearDownDoorController()
    {
        Object.DestroyImmediate(gameObject);
    }
}
