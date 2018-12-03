using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ticket.Bin;
using Ticket.Items;
using Ticket.GeneralMovement;
using Pathfinding;

namespace Ticket.NPC
{
    /// <summary>
    /// Отвечает за поведение НИПа касательно выбрасывания мусора.
    /// </summary>
    [AddComponentMenu("Ticket/NPC/Garbage disposal behaviour")]
    public class GarbageDisposalBehaviour : MonoBehaviour
    {
        [SerializeField] Mover npcMover;
        [SerializeField] ItemDB itemDB;
        [SerializeField] AIPath binsSearchingAI;
        [SerializeField] Seeker binsSearchingSeeker;

        public Action garbageDisposed;

        BinBehaviour[] bins;
        BinBehaviour closestBin;
        Dictionary<BinBehaviour, float> pathLengthByBin = new Dictionary<BinBehaviour, float>();
        bool fillPathLegthsDictionary_CR_running;

        void Awake()
        {
            bins = FindObjectsOfType<BinBehaviour>();
            binsSearchingSeeker.pathCallback += (path) => pathToBin = path;
        }

        public void DisposeTrash()
        {
            StartCoroutine(GoToClosestBin());
        }

        IEnumerator GoToClosestBin()
        {
            fillPathLegthsDictionary_CR_running = true;
            StartCoroutine(FillPathLengthsDictionary());

            yield return new WaitWhile(() => fillPathLegthsDictionary_CR_running == true);

            BinBehaviour currentClosestBin = null;
            float minPathLength = float.PositiveInfinity;

            foreach (KeyValuePair<BinBehaviour, float> entry in pathLengthByBin)
            {
                float pathLengthToBin = entry.Value;
                if (pathLengthToBin < minPathLength)
                {
                    minPathLength = pathLengthToBin;
                    currentClosestBin = entry.Key;
                }
            }

            closestBin = currentClosestBin;
            if (closestBin != null)
            {
                npcMover.ReachedDestination += OnReached;
                npcMover.MoveTo(closestBin.transform.position);  
            }  
        }

        void OnReached()
        {
            StartCoroutine(ThrowTrashInBin());
        }

        IEnumerator ThrowTrashInBin()
        {
            npcMover.ReachedDestination -= OnReached;

            yield return new WaitWhile(() => closestBin == null);

            closestBin.ReceiveItem(itemDB.GetRandomItem());
            if (garbageDisposed != null) garbageDisposed.Invoke();
            pathLengthByBin.Clear();
        }

        Path pathToBin;
        IEnumerator FillPathLengthsDictionary()
        {
            for (int i = 0; i < bins.Length; i++)
            {
                binsSearchingAI.destination = bins[i].transform.position;
                binsSearchingAI.SearchPath();
                yield return new WaitWhile(() => pathToBin == null);

                float pathLength = 0;
                Vector3 lastCorner = (Vector3)pathToBin.path[0].position;

                foreach (GraphNode currentCorner in pathToBin.path)
                {
                    pathLength += Vector3.Distance(lastCorner, (Vector3)currentCorner.position);
                    lastCorner = (Vector3)currentCorner.position;
                }

                pathLengthByBin.Add(bins[i], pathLength);

                pathToBin = null;
            }

            fillPathLegthsDictionary_CR_running = false;
        }
    }
}
