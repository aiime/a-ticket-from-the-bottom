using System.Collections;
using UnityEngine;
using Ticket.Inventory;

namespace Ticket.Items
{
    public class ItemPrefabSpawner : MonoBehaviour
    {
        [SerializeField] GameObject itemPrefab;
        [SerializeField] float spawnInterval;
        [SerializeField] Mover mover;
        [SerializeField] InventoryModel inventory;
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform itemParent;

        private void Awake()
        {
            name = itemPrefab.GetComponent<ItemBehaviour>().item.name + " spawner";
        }

        private void Start()
        {
            StartCoroutine(SpawnItem());
        }

        IEnumerator SpawnItem()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);

                GameObject itemPrefab = 
                    Instantiate(this.itemPrefab, this.transform.position, Quaternion.identity, itemParent);

                ItemBehaviour itemPrefabBottleClickBehaviour = 
                    itemPrefab.GetComponent<ItemBehaviour>();

                itemPrefabBottleClickBehaviour.mover = mover;
                itemPrefabBottleClickBehaviour.inventory = inventory;
                itemPrefabBottleClickBehaviour.playerTransform = playerTransform;
            }
        }
    }
}
