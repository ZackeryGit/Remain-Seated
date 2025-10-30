using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Monster_Controller : MonoBehaviour
{
    [Header("Navmesh Target")]
    public Transform target;
    private NavMeshPath path;
    public NavMeshAgent agent;
    
    [Header("Movement Settings")]
    public float elapsed = 0.0f;
    public float moveTime = 1f;
    
    [Header("Roaming Settings")]
    [Tooltip("The range of Detection is the scale of the Detection range sphere set on the monster")]
    public Transform detectionRange;
    public bool followPlayer = false;

    [Header("Animation")]
    public Animator animator;
    public string roamingAnimationParameter = "isWalking";
    public string chasingAnimationParameter = "isChasing";
    
    public UnityEvent onRoaming;
    public UnityEvent onChasing;
    
    public float detectionRadius = 20f;
    public void Start()
    {
        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void FixedUpdate()
    {
        if(followPlayer)
        {
            FollowPlayer();
        }
        else
        {
            RoamingStart();
        }
    }

    private void RoamingPositionSet()
    { 
        
        Vector3 randomDirection = Random.insideUnitSphere * detectionRadius;
        randomDirection.y = 0f;
        Vector3 roamingPosition = detectionRange.position + randomDirection;
        
        agent.SetDestination(roamingPosition);
        
    }

    private void FollowPlayer()
    {
        onChasing.Invoke();
        animator.Play(chasingAnimationParameter);
        
        elapsed += Time.deltaTime;
        if (elapsed > moveTime)
        {
            elapsed -= moveTime;
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            agent.SetDestination(target.position);
            
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.green);
        
    }
    
    private void RoamingStart()
    {
        onRoaming.Invoke();
        animator.Play(roamingAnimationParameter);
        
        elapsed += Time.deltaTime;
        if (elapsed > moveTime)
        {
            elapsed -= moveTime;
            RoamingPositionSet();
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        
    }
    
}