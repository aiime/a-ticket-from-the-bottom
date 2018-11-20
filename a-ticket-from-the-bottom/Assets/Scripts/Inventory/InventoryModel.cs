using System;
using System.Collections.Generic;
using UnityEngine;
using Ticket.Items;

namespace Ticket.Inventory
{
    [AddComponentMenu("Ticket/Inventory/Inventory Model")]
    public class InventoryModel : MonoBehaviour
    {
        public int capacity;
        private List<Item> inventory;

        public Action<int, Item> ItemAdded; // int - индекс ячейки; Item - добавленный предмет.
        public Action<int, Item> ItemRemoved; // int - индекс ячейки; Item - удалённый предмет.

        private void Awake()
        {
            inventory = new List<Item>(capacity);
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
                    inventory[i] = item.Clone() as Item;
                    if (ItemAdded != null) ItemAdded.Invoke(i, item);
                    return;
                }
            }
        }

        public Item RemoveItem(int i)
        {
            inventory[i] = null;
            if (ItemRemoved != null) ItemRemoved.Invoke(i, inventory[i]);
            return inventory[i];
        }
    }
}
