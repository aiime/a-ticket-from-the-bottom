using System;
using UnityEngine;

namespace Ticket.Items
{
    public abstract class Item : MonoBehaviour, ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
