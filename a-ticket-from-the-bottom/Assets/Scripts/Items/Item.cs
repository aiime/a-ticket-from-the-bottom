using System;
using UnityEngine;

namespace Ticket.Items
{
    public abstract class Item : MonoBehaviour, ICloneable
    {
        public string Name { get; protected set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
