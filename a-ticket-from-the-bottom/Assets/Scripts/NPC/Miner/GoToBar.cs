using System;
using UnityEngine;
using Ticket.GeneralMovement;

namespace Ticket.NPC.Miner
{
    [AddComponentMenu("Ticket/NPC/Miner/Go to bar")]
    public class GoToBar : Task
    {
        [SerializeField] Mover npcMover;
        [SerializeField] Transform barEntrance;

        public override Action TaskComplete { get; set; }

        public override void DoTask()
        {
            npcMover.ReachedDestination += ReachedBar;
            npcMover.MoveTo(barEntrance.position);
        }

        void ReachedBar()
        {
            npcMover.ReachedDestination -= ReachedBar;
            if (TaskComplete != null) TaskComplete.Invoke();
        }
    }
}
