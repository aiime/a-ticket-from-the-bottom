using UnityEngine;
using Ticket.GeneralMovement;

public class GroundClickBehaviour : MonoBehaviour, IClickable
{
    [SerializeField] private Mover mover;

    public void OnClick(GameObject obj, Vector3 point)
    {
        mover.MoveTo(point);
    }
}
