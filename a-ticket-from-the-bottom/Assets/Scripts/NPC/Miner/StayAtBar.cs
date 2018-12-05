using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ticket.Clock;
using System;

namespace Ticket.NPC.Miner
{
    /// <summary>
    /// WARNING! Скрипт подразумевает, НИП окажется в баре до его закрытия.
    /// </summary>
    [AddComponentMenu("Ticket/NPC/Miner/Stay at bar")]
    public class StayAtBar :Task
    {
        [SerializeField] ClockModel clock;
        [SerializeField] int barClosingHour;
        [SerializeField] int barClosingMinutes;

        public override Action TaskComplete { get; set; }

        public override void DoTask()
        {
            clock.TimeUpdated += CheckForBarClosing;
        }

        void CheckForBarClosing(int hour, int minutes)
        {
            if (hour == barClosingHour && minutes == barClosingMinutes)
            {
                clock.TimeUpdated -= CheckForBarClosing;
                if (TaskComplete != null) TaskComplete.Invoke();
            }
        }
    }
}
