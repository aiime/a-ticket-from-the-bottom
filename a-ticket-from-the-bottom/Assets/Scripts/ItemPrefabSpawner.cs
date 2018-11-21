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

        private void Awake()
        {
            name = itemPrefab.GetComponent<BottleClickBehaviour>().item.name + " spawner";
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

                GameObject itemPrefab = Instantiate(this.itemPrefab, this.transform);

                BottleClickBehaviour itemPrefabBottleClickBehaviour = 
                    itemPrefab.GetComponent<BottleClickBehaviour>();

                itemPrefabBottleClickBehaviour.mover = mover;
                itemPrefabBottleClickBehaviour.inventory = inventory;
                itemPrefabBottleClickBehaviour.playerTransform = playerTransform;
            }
        }
    }
}
