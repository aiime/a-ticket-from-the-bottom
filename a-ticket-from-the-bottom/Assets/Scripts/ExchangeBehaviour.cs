using UnityEngine;
using Ticket.Inventory;

namespace Ticket.Click
{
    public class ShopBehaviour : MonoBehaviour, IClickable
    {
        [SerializeField] private Mover playerMover;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform shopTransform;
        [SerializeField] private InventoryView inventoryView;
        [SerializeField] private CanvasGroup inventoryCG;

        public void OnClick(GameObject clickedObject, Vector3 clickPoint)
        {
            playerMover.MoveTo(clickedObject.transform.position);

            playerMover.MovementEnd += AgentNoLongerMovesToShop;
            playerMover.TargetReached += AgentReachedShop;
        }

        private void AgentNoLongerMovesToShop()
        {
            playerMover.MovementEnd -= AgentNoLongerMovesToShop;
            playerMover.TargetReached -= AgentReachedShop;
        }

        private void AgentReachedShop()
        {
            playerMover.MovementEnd -= AgentNoLongerMovesToShop;
            playerMover.TargetReached -= AgentReachedShop;

            Quaternion rotationTowardShop = 
                Quaternion.LookRotation(shopTransform.position - playerTransform.position);

            // Нужно, чтобы модель игрока просто развернулась по 'y' в сторону окна магазина, а не задиралась
            // дополнительно вверх по 'x' и 'z' по навравлению в центр окна. Поэтому обнуляем их.
            rotationTowardShop.x = 0;
            rotationTowardShop.z = 0;

            playerTransform.rotation = rotationTowardShop;

            ActivateShopMode();
        }

        private void ActivateShopMode()
        {
            inventoryCG.interactable = true;
            playerMover.MovementStart += AgentLeavesShop;
        }

        private void AgentLeavesShop()
        {
            playerMover.MovementStart -= AgentLeavesShop;
            DeactivateShopMode();
        }

        private void DeactivateShopMode()
        {
            inventoryCG.interactable = false;
        }
    }
}
