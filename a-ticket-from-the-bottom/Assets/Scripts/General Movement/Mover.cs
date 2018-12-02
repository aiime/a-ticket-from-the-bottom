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
    [RequireComponent(typeof(Seeker))]
    [AddComponentMenu("Ticket/General Movement/Mover")]
    public class Mover : MonoBehaviour
    {
        [SerializeField] float speed;

        public Action WentToDestination;
        public Action ReachedDestination;

        AIPath ai;
        Seeker seeker;
        bool aiMoves;

        void Awake()
        {
            ai = GetComponent<AIPath>();
            seeker = GetComponent<Seeker>();
        }

        public void MoveTo(Vector3 destination)
        {
            if (aiMoves) return;

            aiMoves = true;
            ai.canMove = true;
            ai.destination = destination;
            ai.SearchPath();
            StartCoroutine(WaitForPathComplete());
        }

        IEnumerator WaitForPathComplete()
        {
            while (ai.pathPending) yield return null;

            if (WentToDestination != null) WentToDestination.Invoke();
            StartCoroutine(WaitForDestinationReached());
        }

        IEnumerator WaitForDestinationReached()
        {
            while (!ai.reachedEndOfPath) yield return null;

            aiMoves = false;
            ai.canMove = false;
            if (ReachedDestination != null) ReachedDestination.Invoke();
        }
    }
}
