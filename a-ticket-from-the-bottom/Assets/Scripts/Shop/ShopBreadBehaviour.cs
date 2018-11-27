using UnityEngine;
using Ticket.Health;
using Ticket.Universals;

namespace Ticket.Shop
{
    [AddComponentMenu("Ticket/Shop/Shop bread behaviour")]
    public class ShopBreadBehaviour : MonoBehaviour
    {
        [SerializeField] UniversalsModel universalsModel;
        [SerializeField] HealthModel healthModel;

        public void BuyBread()
        {
            if (universalsModel.Universals >= 30)
            {
                universalsModel.Universals -= 30;
                healthModel.Health += 40;
            }
        }
    }
}
