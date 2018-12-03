using UnityEngine;
using Ticket.GeneralMovement;

namespace Ticket.Click
{
    [AddComponentMenu("Ticket/Exchange/Exchange behaviour")]
    [RequireComponent(typeof(MovementTarget))]
    public class ExchangeBehaviour : MonoBehaviour
    {
        [SerializeField] Mover playerMover;
        [SerializeField] CanvasGroup inventoryCG;

        MovementTarget movementTarget;

        void Awake()
        {
            movementTarget = GetComponent<MovementTarget>();
            movementTarget.TargetReached += ActivateExchangeMode;
        }

        void ActivateExchangeMode()
        {
            inventoryCG.interactable = true;
            playerMover.WentToDestination += PlayerLeavesShop;
        }

        void PlayerLeavesShop()
        {
            playerMover.WentToDestination -= PlayerLeavesShop;
            DeactivateExchangeMode();
        }

        void DeactivateExchangeMode()
        {
            inventoryCG.interactable = false;
        }
    }
}
