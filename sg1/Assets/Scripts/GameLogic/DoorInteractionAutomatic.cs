using UnityEngine;

public class DoorInteractionAutomatic : MonoBehaviour
{
    private DoorController doorController = null;

    // Update is called once per frame
    void Update()
    {
        // if in range of a doorController, continuously try to open the door
        // there is effectively a cooldown with the animation, so the player 
        // has a little time to get away
        if(doorController != null) { 
            doorController.openDoor();
        }        
    }

    public void OnTriggerEnter(Collider collider) {
        // give the player a reference to the doorController when it enters the trigger.
        if(collider.CompareTag("Door")) {
            doorController = collider.GetComponentInParent<DoorController>();
        }
    }

    public void OnTriggerExit(Collider collider) {
        // remove the player's reference to the doorController when it leaves the collider.
        if(collider.CompareTag("Door")) {
            Debug.Log("trigger enter: door");
            doorController = null;
        }
    }

}
