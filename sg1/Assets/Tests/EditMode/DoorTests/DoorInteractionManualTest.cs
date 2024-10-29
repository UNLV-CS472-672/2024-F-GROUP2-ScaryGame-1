using System.Collections;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DoorInteractionManualTest
{
    // GameObject taking role of door
    private GameObject doorGameObject;
    private BoxCollider boxCollider;
    private DoorController doorController;

    // GameObject taking role of Player
    private GameObject controllingObject;
    private DoorInteractionManual doorInteractor;

    private MockDoorAnimator mockDoorAnimator; // Simulates Animator Controller
    private bool mouseDown; // Used to simulate mouse presses


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

        // Add reference to mock animator
        mockDoorAnimator = new MockDoorAnimator(true, "closed");
        doorController.animator = mockDoorAnimator.Object;

        // Set up fake player
        controllingObject = new GameObject();
        controllingObject.AddComponent<DoorInteractionManual>();
        doorInteractor = controllingObject.GetComponent<DoorInteractionManual>();
        doorInteractor.doorController = doorController;

        // Set up mock user input
        mouseDown = false;
        var mockInput = new Mock<IInput>();
        mockInput.Setup(m => m.GetMouseButtonDown(0)).Returns(() => mouseDown);
        doorInteractor.input = mockInput.Object;
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
    public void Update_DoorCloses()
    {
        // Door is open and mouse is down
        mouseDown = true;
        mockDoorAnimator.currentStateName = "open";
        mockDoorAnimator.isClosed = false;

        doorInteractor.Update();

        // Player should close door
        Assert.That(mockDoorAnimator.isClosed, Is.True);
    }

    [Test]
    public void OnTriggerEnter_FindsDoor()
    {
        // Remove Door reference
        doorInteractor.doorController = null;
     
        // Collide
        doorInteractor.OnTriggerEnter(boxCollider);
        
        // Door reference should be added
        Assert.That(doorInteractor.doorController, Is.Not.Null);

        doorInteractor.doorController = doorController;
    }

    [Test]
    public void OnTriggerExit_RemovesDoor()
    {
        // Add door reference
        doorInteractor.doorController = doorController;

        // Leave trigger
        doorInteractor.OnTriggerExit(boxCollider);

        // Door reference should be gone
        Assert.That(doorInteractor.doorController, Is.Null);

        doorInteractor.doorController = doorController;
    }

    [Test]
    public void Start_AddsInputWrapper()
    {
        // Set up new game object
        GameObject testObject = new GameObject();
        testObject.AddComponent<DoorInteractionManual>();
        DoorInteractionManual testDoorInteractor = testObject.GetComponent<DoorInteractionManual>();

        // Call start
        testDoorInteractor.Start();

        // Input reference should have been created
        Assert.That(testDoorInteractor.input, Is.Not.Null);
    }



    [TearDown]
    public void TearDownDoorController()
    {
        Object.DestroyImmediate(doorGameObject);
        Object.DestroyImmediate(controllingObject);

    }
}
