using System;
using UnityEngine;

namespace Ticket.NPC.Miner
{
    [AddComponentMenu("Ticket/NPC/Miner/Go inside dwelling")]
    public class GoInsideDwelling : Task
    {
        [SerializeField] Transform npc;

        public override Action TaskComplete { get; set; }

        public override void DoTask()
        {
            npc.gameObject.SetActive(false);

            if (TaskComplete != null) TaskComplete.Invoke();
        }
    }
}
