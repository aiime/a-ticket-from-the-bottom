using System.Collections;
using UnityEngine;

namespace Ticket.NPC
{
    /// <summary>
    /// Отсюда регулируются все поведения НИПа.
    /// </summary>
    [AddComponentMenu("Ticket/NPC/NPC behaviour")]
    public class NPCBehaviour : MonoBehaviour
    {
        [SerializeField] PatrolBehaviour patrolBehaviour;
        [SerializeField] GarbageDisposalBehaviour garbageDisposalBehaviour;

        private void Start()
        {
            if (patrolBehaviour != null) patrolBehaviour.StartPatrol();
            if (garbageDisposalBehaviour != null) StartCoroutine(DisposeTrashAfterPause());
        }

        IEnumerator DisposeTrashAfterPause()
        {
            int pause = Random.Range(6, 16);
            yield return new WaitForSeconds(pause);
            patrolBehaviour.StopPatrol();
            garbageDisposalBehaviour.DisposeTrash();
            garbageDisposalBehaviour.garbageDisposed += ReturnToPatrol;
        }

        private void ReturnToPatrol()
        {
            garbageDisposalBehaviour.garbageDisposed -= ReturnToPatrol;
            patrolBehaviour.StartPatrol();
            StartCoroutine(DisposeTrashAfterPause());
        }
    }
}
