using UnityEngine;
using UnityEngine.UI;

namespace Ticket.Universals
{
    [AddComponentMenu("Ticket/Universals/Universals view")]
    public class UniversalsView : MonoBehaviour
    {
        [SerializeField] UniversalsModel universalsModel;
        [SerializeField] Text universalsText;

        private void Start()
        {
            universalsModel.UniversalsChanged += UpdateUniversalView;
        }

        private void UpdateUniversalView(int newUnivarsalValue)
        {
            universalsText.text = newUnivarsalValue.ToString();
        }
    }
}
