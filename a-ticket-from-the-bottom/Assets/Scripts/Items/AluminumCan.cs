using UnityEngine;

namespace Ticket.Items
{
    [AddComponentMenu("Ticket/Items/Aluminum Can")]
    public class AluminumCan : Item, ITradable, IItemColor
    {
        public int Cost { get; protected set; }
        public Color32 ItemColor { get; protected set; }

        private void Awake()
        {
            name = "Aluminum can";
            Cost = 40;
            ItemColor = Color.red;
        }
    }
}
