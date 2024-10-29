using System.Collections;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DoorControllerTest
{
    // GameObject taking role of door
    private GameObject gameObject;
    private DoorController doorController;
    private MockDoorAnimator mockDoorAnimator;

    [SetUp]
    public void SetUpDoorController() 
    {
        gameObject = new GameObject();
        gameObject.AddComponent<DoorController>();
        doorController = gameObject.GetComponent<DoorController>();

        mockDoorAnimator = new MockDoorAnimator(true, "closed");
        doorController.animator = mockDoorAnimator.Object;
    }
    [Test]
    public void toggleDoor_NoChange()
    {
        // Door is animating (mismatch between state and isClosed indicates this)
        mockDoorAnimator.currentStateName = "closed";
        mockDoorAnimator.isClosed = false;

        doorController.toggleDoor();

        // Should not change
        Assert.That(mockDoorAnimator.isClosed, Is.False);
    }

    [Test]
    public void toggleDoor_Change()
    {
        // Door is open
        mockDoorAnimator.currentStateName = "open";
        mockDoorAnimator.isClosed = false;

        doorController.toggleDoor();

        // Should be closing
        Assert.That(mockDoorAnimator.isClosed, Is.True);
    }

    [Test]
    public void openDoor_CurrentlyClosed()
    {
        // Door is closed
        mockDoorAnimator.currentStateName = "closed";
        mockDoorAnimator.isClosed = true;

        doorController.openDoor();

        // Should be opening
        Assert.That(mockDoorAnimator.isClosed, Is.False);
    }

    [Test]
    public void openDoor_CurrentlyOpen()
    {
        // Door is open
        mockDoorAnimator.currentStateName = "open";
        mockDoorAnimator.isClosed = false;

        doorController.openDoor();

        // Should stay open
        Assert.That(mockDoorAnimator.isClosed, Is.False);
    }

    [Test]
    public void Start_FindsComponent()
    {
        // Make test object
        GameObject testedObject = new GameObject();
        testedObject.AddComponent<DoorController>();
        DoorController testedController = testedObject.GetComponent<DoorController>();
        testedObject.AddComponent<Animator>();

        testedController.Start();

        // Make sure the animator is found
        Assert.That(testedController.animator, Is.Not.Null);

        Object.DestroyImmediate(testedObject);
    }

    [TearDown]
    public void TearDownDoorController()
    {
        Object.DestroyImmediate(gameObject);
    }
}
