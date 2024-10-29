using System.Collections;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DoorInteractionAutomaticTest
{
    private DoorController doorController;
    private GameObject gameObject;
    private GameObject controllingObject;
    private BoxCollider boxCollider;
    private DoorInteractionAutomatic doorInteractor;
    private string currentStateName;
    private bool wantClosed;

    [SetUp]
    public void SetUpDoorController()
    {
        gameObject = new GameObject();
        gameObject.AddComponent<DoorController>();
        gameObject.AddComponent<BoxCollider>();
        doorController = gameObject.GetComponent<DoorController>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
        gameObject.tag = "Door";

        currentStateName = "closed";
        wantClosed = true;

        var mockAnimator = new Mock<IAnimator>();
        mockAnimator.Setup(m => m.CompareAnimatorStateName(It.IsAny<string>()))
            .Returns((string s) => s == currentStateName);
        mockAnimator.Setup(m => m.SetBool("isClosed", It.IsAny<bool>()))
            .Callback((string s, bool b) => wantClosed = b);
        mockAnimator.Setup(m => m.GetBool("isClosed")).Returns(() => wantClosed);

        doorController.animator = mockAnimator.Object;

        controllingObject = new GameObject();
        controllingObject.AddComponent<DoorInteractionAutomatic>();
        doorInteractor = controllingObject.GetComponent<DoorInteractionAutomatic>();

        doorInteractor.doorController = doorController;
    }

    [Test]
    public void Update_NoDoorStaysClosed()
    {
        doorInteractor.doorController = null;
        currentStateName = "closed";
        wantClosed = true;

        doorInteractor.Update();

        Assert.That(wantClosed, Is.True);

        doorInteractor.doorController = doorController;
    }

    [Test]
    public void Update_DoorOpens()
    {
        currentStateName = "closed";
        wantClosed = true;

        doorInteractor.Update();

        Assert.That(wantClosed, Is.False);
    }

    [Test]
    public void OnTriggerEnter_FindsDoor()
    {
        doorInteractor.doorController = null;
        doorInteractor.OnTriggerEnter(boxCollider);
        Assert.That(doorInteractor.doorController, Is.Not.Null);

        doorInteractor.doorController = doorController;
    }

    [Test]
    public void OnTriggerExit_RemovesDoor()
    {
        doorInteractor.OnTriggerExit(boxCollider);

        Assert.That(doorInteractor.doorController, Is.Null);

        doorInteractor.doorController = doorController;
    }



    [TearDown]
    public void TearDownDoorController()
    {
        Object.DestroyImmediate(gameObject);
        Object.DestroyImmediate(controllingObject);

    }
}
