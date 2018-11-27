using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ticket.PlayerMovement
{
    /// <summary>
    /// Выпускает райкасты по клику мыши и вызывает на встречных объектах метод OnClick.
    /// </summary>
    [AddComponentMenu("Ticket/Player movement/Controll raycaster")]
    public class ControllRaycaster : MonoBehaviour
    {
        public Dictionary<Transform, IClickable> ClickableObjects = new Dictionary<Transform, IClickable>();

        private IClickable clickable;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
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
}
