using UnityEngine;
using UnityEngine.UI;

namespace Ticket.Health
{
    [AddComponentMenu("Ticket/Health/Health View")]
    public class HealthView : MonoBehaviour
    {
        [SerializeField] HealthModel healthModel;
        [SerializeField] Slider healthBar;

        private void Awake()
        {
            healthModel.HealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar(int newHealth)
        {
            healthBar.value = newHealth;
        }
    }
}

