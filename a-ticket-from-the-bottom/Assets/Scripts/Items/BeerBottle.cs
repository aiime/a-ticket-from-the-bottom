using UnityEngine;

namespace Ticket.Items
{
    [AddComponentMenu("Ticket/Items/Beer Bottle")]
    public class BeerBottle : Item, ITradable, IItemColor
    {
        public int Cost { get; protected set; }
        public Color32 ItemColor { get; protected set; }

        private void Awake()
        {
            name = "Beer bottle";
            Cost = 1;
            ItemColor = Color.green;
        }
    }
}
