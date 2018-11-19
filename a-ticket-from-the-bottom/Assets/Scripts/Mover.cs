using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform agentTransform;
    [SerializeField] private float speed;

    public Action<Vector3> MovementStart;
    public Action MovementEnd;
    public Action TargetReached;

    private bool inMotion;

    private void Start()
    {
        agent.updateRotation = false;
    }

    private void Update()
    {
        if (inMotion && agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            agentTransform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }   
    }

    public void Move(Vector3 point)
    {
        if (inMotion)
        {
            if (MovementEnd != null) MovementEnd.Invoke();
        }

        agent.speed = speed;
        agent.acceleration = 60;
        agent.SetDestination(point);
        StartCoroutine(WaitForMovementEnd());
        inMotion = true;
        if (MovementStart != null) MovementStart.Invoke(point);
    }

    IEnumerator WaitForMovementEnd()
    {
        while (true)
        {
            if (agent.pathPending)
            {
                yield return null;
                continue;
            }
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
                continue;
            }
            if (agent.hasPath)
            {
                yield return null;
                continue;
            }
            if (agent.velocity.sqrMagnitude != 0f)
            {
                yield return null;
                continue;
            }

            inMotion = false;
            if (MovementEnd != null) MovementEnd.Invoke();
            if (TargetReached != null) TargetReached.Invoke();
            break;
        }
    }
}
