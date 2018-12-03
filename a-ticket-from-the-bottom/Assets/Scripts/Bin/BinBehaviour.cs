using System.Collections.Generic;
using UnityEngine;
using Ticket.Items;
using Ticket.Inventory;
using Ticket.GeneralMovement;

namespace Ticket.Bin
{
    [AddComponentMenu("Ticket/Bin/Bin behaviour")]
    [RequireComponent(typeof(MovementTarget))]
    public class BinBehaviour : MonoBehaviour
    {
        [SerializeField] InventoryModel inventoryModel;
        [SerializeField] ItemDB itemDB;

        Stack<Item> itemsInside = new Stack<Item>();
        MovementTarget movementTarget;

        void Awake()
        {
            movementTarget = GetComponent<MovementTarget>();
            movementTarget.TargetReached += () => GiveItem();
            ReceiveItem(itemDB.GetRandomItem());
            ReceiveItem(itemDB.GetRandomItem());
            ReceiveItem(itemDB.GetRandomItem());
        }

        public void ReceiveItem(Item item)
        {
            itemsInside.Push(item);
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
