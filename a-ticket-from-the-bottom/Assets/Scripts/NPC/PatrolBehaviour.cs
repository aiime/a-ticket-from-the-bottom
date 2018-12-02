using System.Collections;
using UnityEngine;
using Ticket.GeneralMovement;

namespace Ticket.NPC
{
    [AddComponentMenu("Ticket/NPC/Patrol behaviour")]
    public class PatrolBehaviour : MonoBehaviour
    {
        [SerializeField] Transform[] patrolPath;
        [SerializeField] Mover npcMover;
        [SerializeField] float restTime;

        private int patrolPointIndex;
        private Coroutine restThenMoveCoroutine;

        public void StartPatrol()
        {
            npcMover.ReachedDestination += RestThenMoveFurther;
            npcMover.MoveTo(patrolPath[patrolPointIndex].position);
        }

        public void StopPatrol()
        {
            npcMover.ReachedDestination -= RestThenMoveFurther;
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
