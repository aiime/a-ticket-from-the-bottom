using UnityEngine;
using Ticket.Inventory;
using Ticket.GeneralMovement;

namespace Ticket.Click
{
    public class ExchangeBehaviour : MonoBehaviour, IClickable
    {
        [SerializeField] private Mover playerMover;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform exchangeTransform;
        [SerializeField] private InventoryView inventoryView;
        [SerializeField] private CanvasGroup inventoryCG;

        public void OnClick(GameObject clickedObject, Vector3 clickPoint)
        {
            playerMover.MoveTo(clickedObject.transform.position);

            playerMover.MovementEnd += PlayerNoLongerMovesToShop;
            playerMover.TargetReached += PlayerReachedShop;
        }

        private void PlayerNoLongerMovesToShop()
        {
            playerMover.MovementEnd -= PlayerNoLongerMovesToShop;
            playerMover.TargetReached -= PlayerReachedShop;
        }

        private void PlayerReachedShop()
        {
            playerMover.MovementEnd -= PlayerNoLongerMovesToShop;
            playerMover.TargetReached -= PlayerReachedShop;

            Quaternion rotationTowardExchange = 
                Quaternion.LookRotation(exchangeTransform.position - playerTransform.position);

            // Нужно, чтобы модель игрока просто развернулась по 'y' в сторону окна приёмки, а не задиралась
            // дополнительно вверх по 'x' и 'z' по навравлению в центр окна. Поэтому обнуляем их.
            rotationTowardExchange.x = 0;
            rotationTowardExchange.z = 0;

            playerTransform.rotation = rotationTowardExchange;

            ActivateExchangeMode();
        }

        private void ActivateExchangeMode()
        {
            inventoryCG.interactable = true;
            playerMover.MovementStart += PlayerLeavesShop;
        }

        private void PlayerLeavesShop()
        {
            playerMover.MovementStart -= PlayerLeavesShop;
            DeactivateExchangeMode();
        }

        private void DeactivateExchangeMode()
        {
            inventoryCG.interactable = false;
        }
    }
}
