using System.Collections.Generic;
using UnityEngine;

namespace Ticket.Items
{
    [AddComponentMenu("Ticket/Items/Item DB")]
    public class ItemDB : MonoBehaviour
    {
        public List<Item> gameItems;

        public Item GetItem(string name)
        {
            return gameItems.Find((item) => item.Name == name);
        }

        public Item GetItem(int id)
        {
            return (id < gameItems.Count) ? gameItems[id] : null;
        }

        public Item GetRandomItem()
        {
            return gameItems[Random.Range(0, gameItems.Count)];
        }
    }
}
