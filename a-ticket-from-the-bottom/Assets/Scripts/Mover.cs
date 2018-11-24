﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed;

    public NavMeshAgent agent;
    public Transform agentTransform; 

    public Action MovementStart;
    public Action MovementEnd;
    public Action TargetReached;

    private bool inMotion;
   // private Coroutine waitForMovementEndCoroutine;

    private void Start()
    {
        agent.speed = speed;
        agent.acceleration = 60;
        agent.updateRotation = false;
    }

    private void Update()
    {
        if (inMotion && agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            agentTransform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }

    public void MoveTo(Vector3 point)
    {
        if (inMotion)
        {
            if (MovementEnd != null) MovementEnd.Invoke();
        }
        else
        {
            inMotion = true;
        }
        agent.isStopped = false;
        agent.SetDestination(point);
        StartCoroutine(WaitForMovementEnd());
        if (MovementStart != null) MovementStart.Invoke();
    }

    public void Stop()
    {
        if (MovementEnd != null) MovementEnd.Invoke();
        agent.isStopped = true;
        inMotion = false;
    }

    IEnumerator WaitForMovementEnd()
    {
        while (Vector3.Distance(agentTransform.position, agent.destination) > agent.stoppingDistance)
        {
            yield return null;
        }

        inMotion = false;
        if (TargetReached != null) TargetReached.Invoke();   
    }
}
