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
        [SerializeField] InventoryModel inventory;
        [SerializeField] Transform inventoryPanel;

        private List<Image> bottleRenderers;

        private void Start()
        {
            bottleRenderers = new List<Image>(inventory.capacity);

            for (int i = 0; i < inventory.capacity; i++)
            {
                GameObject bottleSlot = new GameObject("Bottle Slot");
                Image bottleRenderer = bottleSlot.AddComponent<Image>();
                bottleRenderer.sprite = defaultBottle;
                bottleRenderer.color = Color.gray;
                bottleSlot.transform.SetParent(inventoryPanel);

                bottleRenderers.Add(bottleRenderer);
            }

            inventory.ItemAdded += DisplayNewBottle;
            inventory.ItemRemoved += RemoveBottleFromPanel;
        }

        private void DisplayNewBottle(int i, Item bottle)
        {
            if (bottle is IItemColor)
            {
                bottleRenderers[i].color = ((IItemColor)bottle).ItemColor;
            }
        }

        private void RemoveBottleFromPanel(int i, Item bootle)
        {
            bottleRenderers[i].color = Color.gray;
        }
    }
}
