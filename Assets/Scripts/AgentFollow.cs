using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentFollow : MonoBehaviour
{
    [SerializeField] Transform target; // The target position or object to follow
    private NavMeshAgent agent; // Reference to the NavMeshAgent component


    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Ensure NavMeshAgent is properly initialized and active
        if (agent != null && agent.isActiveAndEnabled)
        {
            // Set the initial destination when the script starts
            SetDestination(target.position);
        }
    }

    void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
        // Ensure NavMeshAgent is properly initialized and active
        if (agent != null && agent.isActiveAndEnabled)
        {
            // Check if the agent has reached the destination
            if (agent.remainingDistance < 0.1f)
            {
                // Set a new destination or perform other actions
                SetDestination(target.position);
            }
        }
    }

    void SetDestination(Vector3 destination)
    {
        // Set the destination for the NavMeshAgent to navigate to
        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(destination);
        }
    }
}
