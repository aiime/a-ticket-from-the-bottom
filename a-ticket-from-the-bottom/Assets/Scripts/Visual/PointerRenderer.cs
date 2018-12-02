using UnityEngine;
using UnityEngine.EventSystems;

namespace Ticket.Visual
{
    [AddComponentMenu("Ticket/Visual/Pointer Renderer")]
    public class PointerRenderer : MonoBehaviour
    {
        [SerializeField] Transform pointer;

        bool hitGround;

        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            RaycastHit hit = RaycastGround();

            if (hitGround)
            {
                MovePointer(hit);
            }
        }

        /// <summary>
        /// Возвращает <see cref="RaycastHit"/> по месту попадания. Если попал по земле, то поднимет флаг 
        /// <see cref="hitGround"/>, а иначе опустит.
        /// </summary>
        RaycastHit RaycastGround()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            hitGround = Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Ground"));

            return hit;
        }

        void MovePointer(RaycastHit hit)
        {
            pointer.position = new Vector3((Mathf.Abs((int)hit.point.x) + 0.5f) * Mathf.Sign(hit.point.x),
                                           pointer.position.y,
                                           (Mathf.Abs((int)hit.point.z) + 0.5f) * Mathf.Sign(hit.point.z));
        }
    }
}
