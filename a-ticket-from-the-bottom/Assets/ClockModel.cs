using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ticket.Clock
{
    [AddComponentMenu("Ticket/Clock/Clock Model")]
    public class ClockModel : MonoBehaviour
    {
        [SerializeField] int initialHours;
        [SerializeField] int initialMinutes;
        [SerializeField] float minuteDuration;

        public bool pause;

        /// <summary>
        /// Первый параметр - текущее количество часов, второй параметр - текущее количество минут.
        /// </summary>
        public Action<int, int> TimeUpdated;

        int hours;
        int minutes;
        float currentTime;
        Coroutine clockwork_CR;

        void Awake()
        {
            hours = initialHours;
            minutes = initialMinutes;
        }

        void Start()
        {
            if (TimeUpdated != null) TimeUpdated.Invoke(hours, minutes);
            clockwork_CR = StartCoroutine(Clockwork());
        }

        public void StartClock()
        {
            if (clockwork_CR == null)
            {
                clockwork_CR = StartCoroutine(Clockwork());
            }
            else
            {
                pause = false;
            }
        }

        public void PauseClock()
        {
            pause = true;
        }

        public void StopClock()
        {
            if (clockwork_CR != null)
            {
                StopCoroutine(clockwork_CR);
                hours = 0;
                minutes = 0;
            }
        }

        IEnumerator Clockwork()
        {
            while (true)
            {
                if (!pause)
                {
                    currentTime += Time.deltaTime;

                    if (currentTime >= minuteDuration)
                    {
                        minutes++;
                        currentTime = 0;

                        if (minutes == 60)
                        {
                            hours++;
                            minutes = 0;
                        }

                        if (hours == 24 && minutes == 37)
                        {
                            hours = 0;
                            minutes = 0;
                        }

                        if (TimeUpdated != null) TimeUpdated.Invoke(hours, minutes);
                    }  
                }

                yield return null;
            }
        }
    }
}

