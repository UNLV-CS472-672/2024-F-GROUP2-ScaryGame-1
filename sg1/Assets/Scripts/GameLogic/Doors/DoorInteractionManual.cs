using UnityEngine;

public class DoorInteractionManual : MonoBehaviour
{
    public DoorController doorController = null;
    public IInput input = null;
    public HelpInfo helpInfo;
    private static bool hasEnteredDoor = false;


    public void Start()
    {
        input = new InputWrapper();
    }

    // Update is called once per frame
    public void Update()
    {
        // doorController will only be non-null if the player is in range of a door
        // a (left) mouse click will toggle the door state if it isn't currenlty animating.
        if(doorController != null && input.GetMouseButtonDown(0)) {
            doorController.toggleDoor();
        }
    }
    public void OnTriggerEnter(Collider collider) {
        // There is a trigger collider around the door
        if(collider.CompareTag("Door")) {
            // if the player collides with this, set it's current doorController to 
            // the one matching the collider
            doorController = collider.GetComponentInParent<DoorController>();

            // display a help message the first time the player enters trigger
            if(!hasEnteredDoor)
            {
                // I think it would be annoying if this showed up every time
                hasEnteredDoor = true;
                if(helpInfo != null) 
                    helpInfo.ShowMessage("Click to open door", 3f);
            }
        }
    }

    public void OnTriggerExit(Collider collider) {
        // once the player leaves the collider, remove the reference to it.  
        if(collider.CompareTag("Door")) {
            doorController = null;
        }
    }
}
