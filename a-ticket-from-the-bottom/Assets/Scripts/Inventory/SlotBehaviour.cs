using UnityEngine;
using Ticket.Items;

namespace Ticket.Inventory
{
    [AddComponentMenu("Ticket/Inventory/Slot Behaviour")]
    public class SlotBehaviour : MonoBehaviour
    {
        [HideInInspector] public InventoryModel InventoryModel;
        [HideInInspector] public int SlotNumber;
        [HideInInspector] public UniversalsModel UniversalsBehaviour;

        public void SaleItem()
        {
            Item soldItem = InventoryModel.RemoveItem(SlotNumber);
            if (soldItem != null) UniversalsBehaviour.Universals += soldItem.Cost;
        }
    }
}
