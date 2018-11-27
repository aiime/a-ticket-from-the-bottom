using System;
using System.Collections;
using UnityEngine;

namespace Ticket.Health
{
    [AddComponentMenu("Ticket/Health/Health model")]
    public class HealthModel : MonoBehaviour
    {
        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                if (value <= 0)
                {
                    int lostAmount = health;
                    health = 0;

                    if (HealthLost != null) HealthLost.Invoke(lostAmount);
                    if (PlayerDead != null) PlayerDead.Invoke();
                }

                else if (value > 100)
                {
                    int receivedAmount = 100 - health;
                    health = 100;

                    if (HealthReceived != null) HealthReceived.Invoke(receivedAmount);
                }

                else if (value > health)
                {
                    int receivedAmount = value - health;
                    health = value;

                    if (HealthReceived != null) HealthReceived.Invoke(receivedAmount);
                }

                else if (value < health)
                {
                    int lostAmount = health - value;
                    health = value;

                    if (HealthLost != null) HealthLost.Invoke(lostAmount);
                }

                if (HealthChanged != null) HealthChanged.Invoke(health);
            }
        }

        [SerializeField] float healthLossPeriod;
        [SerializeField] int healthLossAmount;
        [SerializeField] int healthInitial;

        public Action<int> HealthReceived;
        public Action<int> HealthLost;
        public Action<int> HealthChanged;
        public Action PlayerDead;

        private int health;
        private Coroutine healthLossCoroutine;

        private void Start()
        {
            Health = healthInitial;
            PlayerDead += OnPlayerDead;
            healthLossCoroutine = StartCoroutine(HealthLoss());
        }

        private void OnPlayerDead()
        {
            if (healthLossCoroutine != null) StopCoroutine(healthLossCoroutine);
        }

        IEnumerator HealthLoss()
        {
            while (true)
            {
                yield return new WaitForSeconds(healthLossPeriod);
                Health -= healthLossAmount;
            }
        }
    }
}
