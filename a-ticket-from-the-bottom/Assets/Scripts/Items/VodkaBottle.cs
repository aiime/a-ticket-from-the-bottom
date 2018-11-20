using UnityEngine;

namespace Ticket.Items
{
    [AddComponentMenu("Ticket/Items/Vodka Bottle")]
    public class VodkaBottle : Item, ITradable, IItemColor
    {
        public int Cost { get; private set; }
        public Color32 ItemColor { get; protected set; }

        private void Awake()
        {
            name = "Vodka bottle";
            Cost = 5;
            ItemColor = Color.white;
        }
    }
}
