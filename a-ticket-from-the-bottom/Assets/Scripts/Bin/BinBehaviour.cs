using System.Collections.Generic;
using UnityEngine;
using Ticket.Items;
using Ticket.Inventory;

namespace Ticket.Bin
{
    public class BinBehaviour : MonoBehaviour
    {
        [SerializeField] ItemBehaviour itemBehaviour;
        [SerializeField] InventoryModel inventoryModel;

        private Stack<Item> itemsInside = new Stack<Item>();

        private void Awake()
        {
            itemBehaviour.ObjectReached += () => GiveItem();
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
