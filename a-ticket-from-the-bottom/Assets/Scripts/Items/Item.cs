using System;
using UnityEngine;

namespace Ticket.Items
{
    [Serializable]
    public class Item
    {
        [SerializeField] string name;
        [SerializeField] int cost;
        [SerializeField] Color32 color;

        public string Name { get { return name; } }
        public int Cost { get { return cost; } }
        public Color32 Color { get { return color; } }
    }
}
