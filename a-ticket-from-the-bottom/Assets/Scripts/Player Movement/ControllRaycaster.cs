using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Ticket.GeneralMovement;
using Pathfinding;

namespace Ticket.PlayerMovement
{
    /// <summary>
    /// Выпускает райкасты по клику мыши и вызывает на встречных объектах метод OnClick.
    /// </summary>
    [RequireComponent(typeof(Mover))]
    [AddComponentMenu("Ticket/Player movement/Controll raycaster")]
    public class ControllRaycaster : MonoBehaviour
    {
        Mover playerMover;
        bool hitGround;

        private void Awake()
        {
            playerMover = GetComponent<Mover>();
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                RaycastHit hit = RaycastGround();

                if (hitGround)
                {
                    MovePlayer(hit);
                }
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

        void MovePlayer(RaycastHit hit)
        {
            playerMover.MoveTo(hit.point);
        }
    }
}
