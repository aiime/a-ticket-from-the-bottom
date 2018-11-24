using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour, IClickable
{
    [SerializeField] Mover playerMover;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform shopTransform;
    [SerializeField] CanvasGroup shopPanelCG;

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
        playerMover.MovementStart += PlayerLeavesShop;

        shopPanelCG.alpha = 1;
        shopPanelCG.blocksRaycasts = true;
    }

    private void PlayerLeavesShop()
    {
        playerMover.MovementStart -= PlayerLeavesShop;
        DeactivateShopMode();
    }

    private void DeactivateShopMode()
    {
        shopPanelCG.alpha = 0;
        shopPanelCG.blocksRaycasts = false;
    }
}
