using UnityEngine;
using Ticket.GeneralMovement;

public class GroundClickBehaviour : MonoBehaviour, IClickable
{
    [SerializeField] private Mover mover;
    [SerializeField] ControllRaycaster controllRaycaster;

    private void Awake()
    {
        controllRaycaster.ClickableObjects.Add(this.transform, this);
    }

    public void OnClick(GameObject obj, Vector3 point)
    {
        mover.MoveTo(point);
    }
}
