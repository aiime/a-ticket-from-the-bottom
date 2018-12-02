using UnityEngine;
using Ticket.GeneralMovement;
using Ticket.PlayerMovement;

namespace Ticket.Shop
{
    [AddComponentMenu("Ticket/Shop/Shop behaviour")]
    public class ShopBehaviour : MonoBehaviour, IClickable
    {
        [SerializeField] Mover playerMover;
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform shopTransform;
        [SerializeField] CanvasGroup shopPanelCG;
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
            playerMover.WentToDestination += PlayerLeavesShop;

            shopPanelCG.alpha = 1;
            shopPanelCG.blocksRaycasts = true;
        }

        private void PlayerLeavesShop()
        {
            playerMover.WentToDestination -= PlayerLeavesShop;
            DeactivateShopMode();
        }

        private void DeactivateShopMode()
        {
            shopPanelCG.alpha = 0;
            shopPanelCG.blocksRaycasts = false;
        }
    }
}
