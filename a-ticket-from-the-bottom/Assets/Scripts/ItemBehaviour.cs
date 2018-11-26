using System;
using UnityEngine;
using Ticket.GeneralMovement;

public class ItemBehaviour : MonoBehaviour, IClickable
{
    [SerializeField] Mover mover;
    [SerializeField] Transform playerTransform;
    [SerializeField] ControllRaycaster controllRaycaster;

    public Action ObjectReached;

    private bool agentMovesToObject;

    private void Awake()
    {
        controllRaycaster.ClickableObjects.Add(this.transform, this);
    }

    public void OnClick(GameObject clickedObject, Vector3 clickPoint)
    {
        mover.MoveTo(clickedObject.transform.position);
        agentMovesToObject = true;
        mover.MovementEnd += AgentNoLongerMovesToObject;
    }

    private void AgentNoLongerMovesToObject()
    {
        agentMovesToObject = false;
        mover.MovementEnd -= AgentNoLongerMovesToObject;
    }

    /* Используется OnTriggerStay, а не OnTriggerEnter т.к. если объект заспаунится прямо на герое, 
       то OnTriggerEnter при щелчке по предмету не сработает. */
    private void OnTriggerStay(Collider other)
    {
        if (agentMovesToObject && other.gameObject.tag == "Player")
        {
            agentMovesToObject = false;
            mover.Stop();
            playerTransform.rotation = 
                Quaternion.LookRotation(this.transform.position - playerTransform.position);
            if (ObjectReached != null) ObjectReached.Invoke();
        }
    }
}
