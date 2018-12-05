using System;
using UnityEngine;

namespace Ticket.NPC.Miner
{
    [AddComponentMenu("Ticket/NPC/Miner/Appear at mine")]
    public class AppearAtMine : Task
    {
        [SerializeField] Transform mineEntrance;
        [SerializeField] Transform npc;

        public override Action TaskComplete { get; set; }

        public override void DoTask()
        {
            npc.position = mineEntrance.position;
            npc.gameObject.SetActive(true);

            if (TaskComplete != null) TaskComplete.Invoke();
        }
    }
}
