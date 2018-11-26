using System;
using System.Collections.Generic;
using UnityEngine;
using Ticket.Items;

namespace Ticket.Inventory
{
    [AddComponentMenu("Ticket/Inventory/Inventory Model")]
    public class InventoryModel : MonoBehaviour
    {
        public int Capacity;
        private List<Item> inventory;

        public Action<int, Item> ItemAdded; // int - индекс ячейки; Item - добавленный предмет.
        public Action<int, Item> ItemRemoved; // int - индекс ячейки; Item - удалённый предмет.

        private void Awake()
        {
            inventory = new List<Item>(Capacity);
            for (int i = 0; i < inventory.Capacity; i++)
            {
                inventory.Add(null);
            }
        }

        public void AddItem(Item item)
        {
            for (int i = 0; i < inventory.Capacity; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    if (ItemAdded != null) ItemAdded.Invoke(i, item);
                    return;
                }
            }
        }

        public Item RemoveItem(int i)
        {
            if (inventory[i] != null)
            {
                Item removedItem = inventory[i];
                inventory[i] = null;

                if (ItemRemoved != null) ItemRemoved.Invoke(i, removedItem);
                return removedItem;
            }
            else
            {
                return null;
            }
        }
    }
}
