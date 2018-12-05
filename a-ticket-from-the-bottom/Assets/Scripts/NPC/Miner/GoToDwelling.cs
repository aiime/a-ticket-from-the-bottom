using System;
using UnityEngine;
using Ticket.GeneralMovement;

namespace Ticket.NPC.Miner
{
    [AddComponentMenu("Ticket/NPC/Miner/Go to dwelling")]
    public class GoToDwelling : Task
    {
        [SerializeField] Mover npcMover;
        [SerializeField] Transform dwellingEntrance;

        public override Action TaskComplete { get; set; }

        public override void DoTask()
        {
            npcMover.ReachedDestination += ReachedDwelling;
            npcMover.MoveTo(dwellingEntrance.position);
        }

        void ReachedDwelling()
        {
            npcMover.ReachedDestination -= ReachedDwelling;
            if (TaskComplete != null) TaskComplete.Invoke();
        }
    }
}
