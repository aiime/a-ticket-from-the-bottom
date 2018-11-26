using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllRaycaster : MonoBehaviour
{
    public Dictionary<Transform, IClickable> ClickableObjects = new Dictionary<Transform, IClickable>();

    private IClickable clickable;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                if (ClickableObjects.TryGetValue(hit.transform, out clickable))
                {
                    clickable.OnClick(hit.transform.gameObject, hit.point);
                }
            }
        }
    }
}
