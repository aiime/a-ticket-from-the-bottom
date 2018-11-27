using UnityEngine;
using Ticket.Items;
using Ticket.Universals;

namespace Ticket.Inventory
{
    /// <summary>
    /// Вешается на префаб слота инвентаря.
    /// </summary>
    [AddComponentMenu("Ticket/Inventory/Slot Behaviour")]
    public class SlotBehaviour : MonoBehaviour
    {
        [HideInInspector] public InventoryModel InventoryModel;
        [HideInInspector] public int SlotNumber;
        [HideInInspector] public UniversalsModel UniversalsBehaviour;

        /// <summary>
        /// Вызывается нажатием кнопки на слоте.
        /// </summary>
        public void SaleItem()
        {
            Item soldItem = InventoryModel.RemoveItem(SlotNumber);
            if (soldItem != null) UniversalsBehaviour.Universals += soldItem.Cost;
        }
    }
}
