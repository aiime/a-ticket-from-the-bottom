using System.Collections.Generic;
using UnityEngine;
using Ticket.Items;

namespace Ticket.Bin
{
    public class BinBehaviour : MonoBehaviour
    {
        private Stack<Item> itemsInside = new Stack<Item>();

        public void ReceiveItem(Item item)
        {
            itemsInside.Push(item);
            print("received: x1 [" + item.Name + "]");
        }

        public List<Item> GiveItems(int itemsRequired)
        {
            List<Item> itemsToGive = new List<Item>();

            for (int i = 0; (i < itemsRequired) && (i < itemsInside.Count); i++)
            {
                itemsToGive.Add(itemsInside.Pop());
            }

            return itemsToGive;
        }
    }
}
