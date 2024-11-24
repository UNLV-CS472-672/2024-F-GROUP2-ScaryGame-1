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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        float angle1 = transform.Find("bound1").transform.rotation.eulerAngles.z;
        float angle2 = transform.Find("bound2").transform.rotation.eulerAngles.z;
        maxAngle = (angle1 >  angle2) ? angle1 : angle2;
        minAngle = (angle1 < angle2) ? angle1 : angle2;

        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        dialTransform = transform.Find("dial");
        dialTransform.Rotate(0, 0, Random.Range(0f, 360f));
    }

    public void Rotate()
    {
        dialTransform.Rotate(0, 0, (clockwise ? -1 : 1) * rotationSpeed *  Time.deltaTime);
    }

    public bool InRange()
    {
        return dialTransform.rotation.eulerAngles.z <= maxAngle
            && dialTransform.rotation.eulerAngles.z >= minAngle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
