using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentFollow : MonoBehaviour
{
    [SerializeField] Transform target; // The target position or object to follow
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public float checkRadius;
    private bool isInChaseRange;
    public LayerMask mask;


    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, mask);
        target = GameObject.FindWithTag("Player").transform;
        // Ensure NavMeshAgent is properly initialized and active
        if (agent != null && agent.isActiveAndEnabled && isInChaseRange)
        {
            agent.SetDestination(target.position);
        }

    }

}
