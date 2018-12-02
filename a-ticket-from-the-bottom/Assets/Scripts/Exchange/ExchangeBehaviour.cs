﻿using UnityEngine;
using Ticket.Inventory;
using Ticket.GeneralMovement;
using Ticket.PlayerMovement;

namespace Ticket.Click
{
    [AddComponentMenu("Ticket/Exchange/Exchange behaviour")]
    public class ExchangeBehaviour : MonoBehaviour, IClickable
    {
        [SerializeField] private Mover playerMover;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform exchangeTransform;
        [SerializeField] private InventoryView inventoryView;
        [SerializeField] private CanvasGroup inventoryCG;
        [SerializeField] ControllRaycaster controllRaycaster;

        private void Awake()
        {
            //controllRaycaster.ClickableObjects.Add(this.transform, this);
        }

        public void OnClick(GameObject clickedObject, Vector3 clickPoint)
        {
            playerMover.MoveTo(clickedObject.transform.position);

            playerMover.ReachedDestination += PlayerReachedShop;
        }

        private void PlayerReachedShop()
        {
            playerMover.ReachedDestination -= PlayerReachedShop;

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
            playerMover.WentToDestination += PlayerLeavesShop;
        }

        private void PlayerLeavesShop()
        {
            playerMover.WentToDestination -= PlayerLeavesShop;
            DeactivateExchangeMode();
        }

        private void DeactivateExchangeMode()
        {
            inventoryCG.interactable = false;
        }
    }
}
