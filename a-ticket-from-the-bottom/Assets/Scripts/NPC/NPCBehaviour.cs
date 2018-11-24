﻿using System.Collections;
using UnityEngine;

namespace Ticket.NPC
{
    public class NPCBehaviour : MonoBehaviour
    {
        [SerializeField] PatrolBehaviour patrolBehaviour;
        [SerializeField] GarbageDisposalBehaviour garbageDisposalBehaviour;

        private void Start()
        {
            patrolBehaviour.StartPatrol();
            StartCoroutine(DisposeTrashAfterPause());
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
