﻿using System.Collections;
using UnityEngine;

namespace Ticket.NPC
{
    public class PatrolBehaviour : MonoBehaviour
    {
        [SerializeField] Transform[] patrolPath;
        [SerializeField] Mover npcMover;
        [SerializeField] float restTime;

        private int patrolPointIndex;
        private Coroutine restThenMoveCoroutine;

        public void StartPatrol()
        {
            npcMover.TargetReached += RestThenMoveFurther;
            npcMover.MoveTo(patrolPath[patrolPointIndex].position);
        }

        public void StopPatrol()
        {
            npcMover.TargetReached -= RestThenMoveFurther;
            npcMover.Stop();
            if (restThenMoveCoroutine != null) StopCoroutine(restThenMoveCoroutine);
        }

        private void RestThenMoveFurther()
        {
            if (restThenMoveCoroutine != null) StopCoroutine(restThenMoveCoroutine);
            restThenMoveCoroutine = StartCoroutine(RestThenMove());
        }

        IEnumerator RestThenMove()
        {
            yield return new WaitForSeconds(restTime);
            patrolPointIndex = (patrolPointIndex + 1) % patrolPath.Length;
            npcMover.MoveTo(patrolPath[patrolPointIndex].position);
        }
    }

}