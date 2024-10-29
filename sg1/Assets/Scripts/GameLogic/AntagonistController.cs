using UnityEngine;

public class AntagonistController : MonoBehaviour
{
    // Maximum Speed
    public float speed = 3.0f;
    // Maximum Velocity
    public float maxVelocity = 5.0f;  
    // Distance away from player antagonist should maintain
    public float stopDistance = 2.0f; 
    // Target of Antagonist
    public Transform target;  
    // Rigid Body
    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (target != null)
        {
            // Finding Distance away from Player
            float distance = Vector3.Distance(transform.position, target.position);

            // If larger than defined stopping Distance, move closer
            if (distance > stopDistance)
            {
                // Direction and Force
                Vector3 direction = (target.position - transform.position).normalized;
                rb.AddForce(direction * speed * Time.deltaTime * 400);

                // Clamp velocity so antagonist doesn't bounce back and forth
                rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVelocity);
            }
            else
            {
                // Stops Antagonist so he doesn't continually run at full speed
                rb.linearVelocity = Vector3.zero;
            }
        }
    }
}