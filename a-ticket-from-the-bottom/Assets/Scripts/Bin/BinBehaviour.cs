using System.Collections.Generic;
using UnityEngine;
using Ticket.Items;
using Ticket.Inventory;

namespace Ticket.Bin
{
    [AddComponentMenu("Ticket/Bin/Bin behaviour")]
    public class BinBehaviour : MonoBehaviour
    {
        [SerializeField] MovementTarget movementTarget;
        [SerializeField] InventoryModel inventoryModel;

        private Stack<Item> itemsInside = new Stack<Item>();

        private void Awake()
        {
            movementTarget.ObjectReached += () => GiveItem();
        }

        public void ReceiveItem(Item item)
        {
            itemsInside.Push(item);
            print("received: x1 [" + item.Name + "]");
        }

        public void GiveItem()
        {
            if (itemsInside.Count > 0)
            {
                inventoryModel.AddItem(itemsInside.Pop());
            }
        }
    }
}
