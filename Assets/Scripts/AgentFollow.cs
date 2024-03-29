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
    }

    void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
        // Ensure NavMeshAgent is properly initialized and active
        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(target.position);
        }

    }

}
