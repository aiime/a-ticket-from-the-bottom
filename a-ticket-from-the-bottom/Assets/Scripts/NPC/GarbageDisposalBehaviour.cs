using System;
using UnityEngine;
using UnityEngine.AI;
using Ticket.Bin;
using Ticket.Items;

namespace Ticket.NPC
{
    public class GarbageDisposalBehaviour : MonoBehaviour
    {
        [SerializeField] Mover npcMover;
        [SerializeField] NavMeshAgent npcAgent;
        [SerializeField] Item trashItem;

        public Action garbageDisposed;

        private GameObject[] bins;
        private BinBehaviour binBehaviour;

        private void Awake()
        {
            bins = GameObject.FindGameObjectsWithTag("Bin");
        }

        public void DisposeTrash()
        {
            GoToClosestBin();
        }

        private void GoToClosestBin()
        {
            npcMover.TargetReached += ThrowTrashInBin;
            npcMover.MoveTo(FindClosestBin().transform.position);
        }

        private void ThrowTrashInBin()
        {
            npcMover.TargetReached -= ThrowTrashInBin;
            binBehaviour.ReceiveItem(trashItem);
            if (garbageDisposed != null) garbageDisposed.Invoke();
        }

        private GameObject FindClosestBin()
        {
            GameObject closestBin = new GameObject();
            float minPathLength = float.PositiveInfinity;

            foreach (GameObject bin in bins)
            {
                float pathLengthToBin = GetPathLengthTo(bin.transform.position);
                if (pathLengthToBin < minPathLength)
                {
                    minPathLength = pathLengthToBin;
                    closestBin = bin;
                }
            }

            binBehaviour = closestBin.GetComponent<BinBehaviour>();
            return closestBin;
        }

        private float GetPathLengthTo(Vector3 target)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(npcAgent.transform.position, target, NavMesh.AllAreas, path);

            float pathLength = 0;
            Vector3 lastCorner = path.corners[0];

            foreach (Vector3 currentCorner in path.corners)
            {
                pathLength += Vector3.Distance(lastCorner, currentCorner);
                lastCorner = currentCorner;
            }

            return pathLength;
        }
    }
}
