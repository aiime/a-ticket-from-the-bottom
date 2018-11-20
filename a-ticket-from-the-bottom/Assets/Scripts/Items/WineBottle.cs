using UnityEngine;

namespace Ticket.Items
{
    [AddComponentMenu("Ticket/Items/Wine Bottle")]
    public class WineBottle : Item, ITradable, IItemColor
    {
        public int Cost { get; private set; }
        public Color32 ItemColor { get; private set; }

        private void Awake()
        {
            name = "Wine bottle";
            Cost = 7;
            ItemColor = Color.magenta;
        }
    }
}
