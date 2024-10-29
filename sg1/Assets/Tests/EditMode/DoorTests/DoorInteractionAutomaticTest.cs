using System.Collections;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DoorInteractionAutomaticTest
{
    // GameObject taking role of door
    private GameObject doorGameObject;
    private BoxCollider boxCollider;
    private DoorController doorController;

    // GameObject taking role of Antagonist
    private GameObject controllingObject;
    private DoorInteractionAutomatic doorInteractor;

    private MockDoorAnimator mockDoorAnimator; // Simulates animator controller


    [SetUp]
    public void SetUpDoorController()
    {
        // Set up fake door
        doorGameObject = new GameObject();
        doorGameObject.AddComponent<DoorController>();
        doorGameObject.AddComponent<BoxCollider>();
        doorController = doorGameObject.GetComponent<DoorController>();
        boxCollider = doorGameObject.GetComponent<BoxCollider>();
        doorGameObject.tag = "Door";

        // Add reference to animator
        mockDoorAnimator = new MockDoorAnimator(true, "closed");
        doorController.animator = mockDoorAnimator.Object;

        // Set up fake antagonist
        controllingObject = new GameObject();
        controllingObject.AddComponent<DoorInteractionAutomatic>();
        doorInteractor = controllingObject.GetComponent<DoorInteractionAutomatic>();
        doorInteractor.doorController = doorController;
    }

    [Test]
    public void Update_NoDoorStaysClosed()
    {
        // Remove reference to door controller
        doorInteractor.doorController = null;
        mockDoorAnimator.currentStateName = "closed";
        mockDoorAnimator.isClosed = true;

        doorInteractor.Update();

        // Ensure that state did not change
        Assert.That(mockDoorAnimator.isClosed, Is.True);

        doorInteractor.doorController = doorController;
    }

    [Test]
    public void Update_DoorOpens()
    {
        // Door is closed
        mockDoorAnimator.currentStateName = "closed";
        mockDoorAnimator.isClosed = true;

        doorInteractor.Update();

        // Update should open it
        Assert.That(mockDoorAnimator.isClosed, Is.False);
    }

    [Test]
    public void OnTriggerEnter_FindsDoor()
    {
        // Remove Door reference
        doorInteractor.doorController = null;

        doorInteractor.OnTriggerEnter(boxCollider);

        // Door reference should be added
        Assert.That(doorInteractor.doorController, Is.Not.Null);

        doorInteractor.doorController = doorController;
    }

    [Test]
    public void OnTriggerExit_RemovesDoor()
    {
        // Leave trigger
        doorInteractor.OnTriggerExit(boxCollider);

        // Door reference should be gone
        Assert.That(doorInteractor.doorController, Is.Null);

        doorInteractor.doorController = doorController;
    }



    [TearDown]
    public void TearDownDoorController()
    {
        Object.DestroyImmediate(doorGameObject);
        Object.DestroyImmediate(controllingObject);

    }
}
