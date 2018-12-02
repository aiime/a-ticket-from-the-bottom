using UnityEngine;
using Ticket.GeneralMovement;

namespace Ticket.PlayerMovement
{
    /// <summary>
    /// Вешается на землю и позволяет кликать по ней, перемещая игрока.
    /// </summary>
    [AddComponentMenu("Ticket/Player movement/Ground click behaviour")]
    public class GroundClickBehaviour : MonoBehaviour, IClickable
    {
        [SerializeField] private Mover mover;
        [SerializeField] ControllRaycaster controllRaycaster;

        private void Awake()
        {
            //controllRaycaster.ClickableObjects.Add(this.transform, this);
        }

        public void OnClick(GameObject obj, Vector3 point)
        {
            mover.MoveTo(point);
        }
    }
}
