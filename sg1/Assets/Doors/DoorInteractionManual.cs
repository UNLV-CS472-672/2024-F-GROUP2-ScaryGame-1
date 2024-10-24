using UnityEngine;

public class DoorInteractionManual : MonoBehaviour
{
    private DoorController doorController = null;

    // Update is called once per frame
    void Update()
    {
        // doorController will only be non-null if the player is in range of a door
        // a (left) mouse click will toggle the door state if it isn't currenlty animating.
        if(doorController != null && Input.GetMouseButtonDown(0)) {
            doorController.toggleDoor();
        }
    }
    public void OnTriggerEnter(Collider collider) {
        // There is a trigger collider around the door
        if(collider.CompareTag("Door")) {
            Debug.Log("trigger enter: door");
            // if the player collides with this, set it's current doorController to 
            // the one matching the collider
            doorController = collider.GetComponentInParent<DoorController>();
        }
    }

    public void OnTriggerExit(Collider collider) {
        // once the player leaves the collider, remove the reference to it.  
        if(collider.CompareTag("Door")) {
            doorController = null;
        }
    }
}
