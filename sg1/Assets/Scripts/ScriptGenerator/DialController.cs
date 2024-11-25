using Castle.Components.DictionaryAdapter;
using UnityEngine;

public class DialController : MonoBehaviour
{
    public float minRotationSpeed = 30f;
    public float maxRotationSpeed = 90f;
    public bool clockwise = false;
    public float minAngle = 0f;
    public float maxAngle = 0f;
    public float rotationSpeed;
    public Transform dialTransform;

    // OnEnable is called anytime the GameObject is enabled
    void OnEnable()
    {
        // These are two invisible gameobjects that I use to mark the range of acceptable angles
        float angle1 = transform.Find("bound1").transform.rotation.eulerAngles.z;
        float angle2 = transform.Find("bound2").transform.rotation.eulerAngles.z;
        
        // Find max and min. This won't work if intended range of angles spans 0
        maxAngle = (angle1 >  angle2) ? angle1 : angle2;
        minAngle = (angle1 < angle2) ? angle1 : angle2;

        // pick a random rotation speed
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        
        dialTransform = transform.Find("dial");

        // Pick a random start position
        dialTransform.Rotate(0, 0, Random.Range(0f, 360f));
    }

    // Rotates the dial by rotationSpeed per second
    public void Rotate()
    {
        dialTransform.Rotate(0, 0, (clockwise ? -1 : 1) * rotationSpeed *  Time.deltaTime);
    }

    // Checks minAngle <= angle <= maxAngle
    public bool InRange()
    {
        return dialTransform.rotation.eulerAngles.z <= maxAngle
            && dialTransform.rotation.eulerAngles.z >= minAngle;
    }
}
