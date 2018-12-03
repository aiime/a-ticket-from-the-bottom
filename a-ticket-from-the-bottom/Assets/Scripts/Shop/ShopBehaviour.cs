using UnityEngine;
using Ticket.GeneralMovement;

namespace Ticket.Shop
{
    [AddComponentMenu("Ticket/Shop/Shop behaviour")]
    [RequireComponent(typeof(MovementTarget))]
    public class ShopBehaviour : MonoBehaviour
    {
        [SerializeField] Mover playerMover;
        [SerializeField] CanvasGroup shopPanelCG;

        MovementTarget movementTarget;

        void Awake()
        {
            movementTarget = GetComponent<MovementTarget>();
            movementTarget.TargetReached += ActivateShopMode;
        }

        void ActivateShopMode()
        {
            playerMover.WentToDestination += PlayerLeavesShop;

            shopPanelCG.alpha = 1;
            shopPanelCG.blocksRaycasts = true;
        }

        void PlayerLeavesShop()
        {
            playerMover.WentToDestination -= PlayerLeavesShop;
            DeactivateShopMode();
        }

        void DeactivateShopMode()
        {
            shopPanelCG.alpha = 0;
            shopPanelCG.blocksRaycasts = false;
        }
    }
}
