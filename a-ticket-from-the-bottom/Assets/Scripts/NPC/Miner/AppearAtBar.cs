using System;
using UnityEngine;

namespace Ticket.NPC.Miner
{
    [AddComponentMenu("Ticket/NPC/Miner/Appear at bar")]
    public class AppearAtBar : Task
    {
        [SerializeField] Transform barEntrance;
        [SerializeField] Transform npc;

        public override Action TaskComplete { get; set; }

        public override void DoTask()
        {
            npc.position = barEntrance.position;
            npc.gameObject.SetActive(true);

            if (TaskComplete != null) TaskComplete.Invoke();
        }
    }
}
