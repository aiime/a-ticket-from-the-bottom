using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Ticket.GeneralMovement
{
    /// <summary>
    /// Двигает объект с помощью метода <see cref="MoveTo(Vector3)"/>. Имеет события <see cref="WentToDestination"/> и
    /// <see cref="ReachedDestination"/>, сообщающие о статусе движения.
    /// </summary>
    [RequireComponent(typeof(AIPath))]
    [AddComponentMenu("Ticket/General Movement/Mover")]
    public class Mover : MonoBehaviour
    {
        [SerializeField] float speed;

        public Action WentToDestination;
        public Action ReachedDestination;
        public Action ChangedDestination;
        public MovementTarget CurrentMovementTarget { get; private set; }

        AIPath ai;
        Seeker seeker;

        void Awake()
        {
            ai = GetComponent<AIPath>();
            seeker = GetComponent<Seeker>();
        }

        public void MoveTo(Vector3 destination)
        {
            ai.destination = destination;
            if (ChangedDestination != null) ChangedDestination.Invoke();
            ai.SearchPath();
            StartCoroutine(WaitForPathComplete());
        }

        public void MoveTo(MovementTarget movementTarget)
        {
            if (movementTarget == CurrentMovementTarget)
            {
                return;
            }

            CurrentMovementTarget = movementTarget;
            ai.destination = movementTarget.transform.position;
            if (ChangedDestination != null) ChangedDestination.Invoke();
            ai.SearchPath();
            StartCoroutine(WaitForPathComplete());
        }

        IEnumerator WaitForPathComplete()
        {
            while (ai.pathPending) yield return null;

            // Проверяем, не стоит ли ИИ на клетке, по которой щёлкнули.
            List<GraphNode> path = seeker.GetCurrentPath().path;
            Vector3 pathEnd = (Vector3)path[path.Count - 1].position;
            if (Vector3.Distance(ai.position, pathEnd) > ai.endReachedDistance)
            {
                // Если не стоит, то движемся.
                ai.canMove = true;
            }
            else
            {
                // Если стоит, то удаляем путь и не движемся.
                ai.enabled = false;
                ai.enabled = true;
                CurrentMovementTarget = null;
                if (WentToDestination != null) WentToDestination.Invoke();
                if (ReachedDestination != null) ReachedDestination.Invoke();
                yield break;
            }

            if (WentToDestination != null) WentToDestination.Invoke();
            StartCoroutine(WaitForDestinationReached());
        }

        IEnumerator WaitForDestinationReached()
        {
            while (!ai.reachedEndOfPath) yield return null;

            ai.canMove = false;
            CurrentMovementTarget = null;
            if (ReachedDestination != null) ReachedDestination.Invoke();
        }
    }
}
