using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ticket.Items;

namespace Ticket.Inventory
{
    [AddComponentMenu("Ticket/Inventory/Inventory View")]
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] Sprite defaultBottle;
        [SerializeField] InventoryModel inventoryModel;
        [SerializeField] Transform inventoryPanel;
        [SerializeField] GameObject bottleSlotPrefab;
        [SerializeField] UniversalsModel universalsModel;

        public List<GameObject> BottleSlots;

        private List<Image> bottleRenderers;

        private void Start()
        {
            BottleSlots = new List<GameObject>();
            bottleRenderers = new List<Image>(inventoryModel.Capacity);

            for (int i = 0; i < inventoryModel.Capacity; i++)
            {
                GameObject bottleSlot = Instantiate(bottleSlotPrefab, inventoryPanel);
                bottleSlot.GetComponent<SlotBehaviour>().SlotNumber = i;
                bottleSlot.GetComponent<SlotBehaviour>().InventoryModel = inventoryModel;
                bottleSlot.GetComponent<SlotBehaviour>().UniversalsBehaviour = universalsModel;

                BottleSlots.Add(bottleSlot);
                bottleRenderers.Add(bottleSlot.GetComponent<Image>());
            }

            inventoryModel.ItemAdded += DisplayAddedBottle;
            inventoryModel.ItemRemoved += RemoveBottleFromPanel;
        }

        private void DisplayAddedBottle(int i, Item bottle)
        {
            bottleRenderers[i].color = bottle.Color;
        }

        private void RemoveBottleFromPanel(int i, Item bootle)
        {
            bottleRenderers[i].color = Color.gray;
        }
    }
}
