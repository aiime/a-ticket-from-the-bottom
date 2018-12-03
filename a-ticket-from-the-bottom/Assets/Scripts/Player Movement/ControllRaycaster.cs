using UnityEngine;
using UnityEngine.EventSystems;
using Ticket.GeneralMovement;

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

        private void Awake()
        {
            playerMover = GetComponent<Mover>();
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                RaycastHit hit = RaycastMousePosition();

                if (hit.transform.tag == "Movement Target")
                {
                    MovementTarget movementTarget = hit.transform.gameObject.GetComponent<MovementTarget>();
                    movementTarget.WaitForArrival();
                    if (movementTarget.hasAlternativeDestination)
                    {
                        playerMover.MoveTo(movementTarget.alternativeDestination.position);
                    }
                    else
                    {
                        playerMover.MoveTo(movementTarget);
                    } 
                }

                if (hit.transform.tag == "Ground")
                {
                    playerMover.MoveTo(hit.point);
                }
            }   
        }

        RaycastHit RaycastMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            Physics.Raycast(ray, out hit);

            return hit;
        }
    }
}
