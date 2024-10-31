using UnityEngine;
using UnityEngine.AI;

public class NewAntagonistController : MonoBehaviour
{
    public Transform playerTransform;
    // Uses the NavMesh system. 
    // The NavMesh surface is on the Map Core prefab
    NavMeshAgent agent;

    // Length of time the antagonist will chase without seeing the player. 
    float chasingTimeLimit = 5.0f;
    float chasingTimer = 0;

    // Interval between path recalulation when patrolling
    float patrollingRefindTimeLimit = 10.0f;
    float chasingRefindTimeLimit = 0.5f; // when chasing
    float refindTimer = 0;
    enum PathFindingState
    {
        Chasing, Patrolling
    }
    PathFindingState pathFindingState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTransform.position;
        pathFindingState = PathFindingState.Patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        if (PathFindingState.Patrolling == pathFindingState)
        {
            // Start chasing when the player is spotted
            if (InLineOfSight())
            {
                pathFindingState = PathFindingState.Chasing;
                chasingTimer = 0;
                refindTimer = 0;
                Debug.Log("Chasing");
            }
            else
            {
                // reset destination if enough time has passed
                refindTimer += Time.deltaTime;
                if (refindTimer > patrollingRefindTimeLimit)
                {
                    agent.destination = playerTransform.position;
                    refindTimer = 0;
                }
            }
        }

        if (PathFindingState.Chasing == pathFindingState)
        {
            // Keep track of how long it has been since the player has been seen
            if (InLineOfSight())
            {
                chasingTimer = 0;
            }
            else
            {
                chasingTimer += Time.deltaTime;
            }

            refindTimer += Time.deltaTime;

            // Revert to patrolling if it has been too long since the player was seen
            if (chasingTimer > chasingTimeLimit)
            {
                pathFindingState = PathFindingState.Patrolling;
                chasingTimer = 0;
                refindTimer = 0;
                Debug.Log("Patrolling");
            }
            else if(refindTimer > chasingRefindTimeLimit)
            {
                agent.destination = playerTransform.position;
                refindTimer = 0;
            }
        }
    }

    // This is not a real line of sight check, just checks if there is a short enough unobstructed path.
    bool InLineOfSight()
    {
        RaycastHit hit;

        // ai-gen start (GPT-4o mini, 0)
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        // ai-gen end

        if (Physics.Raycast(transform.position, playerDirection, out hit, 10.0f, -1, QueryTriggerInteraction.Ignore))
        {
            return hit.collider.name == "Player";
        }

        return false;
    }
}
