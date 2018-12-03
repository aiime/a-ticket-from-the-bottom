using UnityEngine;

namespace Ticket.Shop
{
    /// <summary>
    /// Двигает UI панель магазина, чтобы она всегда весела над ним. Камера для отрисовки панели магазина
    /// находится в режиме оверлея, поэтому приходится так делать.
    /// </summary>
    [ExecuteInEditMode]
    [AddComponentMenu("Ticket/Shop/Shop panel positioner")]
    public class ShopPanelPositioner : MonoBehaviour
    {
        [SerializeField] Transform shop;
        [SerializeField] RectTransform shopPanel;
        [SerializeField] Vector3 panelOffset;

        void LateUpdate()
        {
            shopPanel.position = Camera.main.WorldToScreenPoint(shop.position) + panelOffset;
        }
    }
}

