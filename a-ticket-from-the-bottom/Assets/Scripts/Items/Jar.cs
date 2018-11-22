using UnityEngine;

namespace Ticket.Items
{
    [AddComponentMenu("Ticket/Items/Jar")]
    public class Jar : Item, ITradable, IItemColor
    {
        public int Cost { get; protected set; }
        public Color32 ItemColor { get; protected set; }

        private void Awake()
        {
            Name = "Jar";
            Cost = 18;
            ItemColor = Color.cyan;
        }
    }
}
