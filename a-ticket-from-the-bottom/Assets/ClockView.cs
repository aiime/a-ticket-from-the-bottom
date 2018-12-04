using UnityEngine;
using UnityEngine.UI;

namespace Ticket.Clock
{
    [AddComponentMenu("Ticket/Clock/Clock View")]
    [RequireComponent(typeof(ClockModel))]
    public class ClockView : MonoBehaviour
    {
        [SerializeField] Text hours0;
        [SerializeField] Text hours1;
        [SerializeField] Text minutes0;
        [SerializeField] Text minutes1;

        ClockModel clockModel;

        void Awake()
        {
            clockModel = GetComponent<ClockModel>();
            clockModel.TimeUpdated += UpdateTimerDisplay;
        }

        void UpdateTimerDisplay(int hours, int minutes)
        {
            string hoursStr = hours.ToString().PadLeft(2, '0');
            string minutesStr = minutes.ToString().PadLeft(2, '0');

            hours0.text = hoursStr[0].ToString();
            hours1.text = hoursStr[1].ToString();
            minutes0.text = minutesStr[0].ToString();
            minutes1.text = minutesStr[1].ToString();
        }
    }
}
