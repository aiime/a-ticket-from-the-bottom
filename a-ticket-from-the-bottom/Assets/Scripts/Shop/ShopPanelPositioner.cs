using UnityEngine;

[ExecuteInEditMode]
public class ShopPanelPositioner : MonoBehaviour
{
    [SerializeField] Transform shopWindow;
    [SerializeField] RectTransform shopPanel;
    [SerializeField] Vector3 panelOffset;

	void LateUpdate ()
    {
        shopPanel.position = Camera.main.WorldToScreenPoint(shopWindow.position) + panelOffset;
    }
}
