using System;
using UnityEngine;
using Ticket.Clock;

namespace Ticket.NPC.Miner
{
    [AddComponentMenu("Ticket/NPC/Miner/Stay at dwelling")]
    public class StayAtDwelling : Task
    {
        [SerializeField] ClockModel clock;
        [SerializeField] int goToWorkHour;
        [SerializeField] int goToWorkMinutes;

        public override Action TaskComplete { get; set; }

        public override void DoTask()
        {
            clock.TimeUpdated += CheckWorkTime;
        }

        void CheckWorkTime(int hour, int minutes)
        {
            if (hour == goToWorkHour && minutes == goToWorkMinutes)
            {
                clock.TimeUpdated -= CheckWorkTime;
                if (TaskComplete != null) TaskComplete.Invoke();
            }
        }
    }
}
