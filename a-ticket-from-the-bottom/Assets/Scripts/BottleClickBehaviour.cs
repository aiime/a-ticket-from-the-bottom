using UnityEngine;
using Ticket.Items;
using Ticket.Inventory;

public class BottleClickBehaviour : MonoBehaviour, IClickable
{
    [SerializeField] private Mover mover;
    [SerializeField] InventoryModel inventory;
    [SerializeField] Item item;

    private bool agentMovesToItem;

    public void OnClick(GameObject clickedObject, Vector3 clickPoint)
    {
        mover.MoveTo(clickedObject.transform.position);

        agentMovesToItem = true;
        mover.MovementEnd += AgentNoLongerMovesToItem;
        mover.TargetReached += AgentNoLongerMovesToItem;
    }

    private void AgentNoLongerMovesToItem()
    {
        agentMovesToItem = false;

        mover.MovementEnd -= AgentNoLongerMovesToItem;
        mover.TargetReached -= AgentNoLongerMovesToItem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (agentMovesToItem)
        {
            mover.Stop();
            inventory.AddItem(item);
            Destroy(transform.gameObject);
        } 
    }

    private void OnDestroy()
    {
        mover.MovementEnd -= AgentNoLongerMovesToItem;
        mover.TargetReached -= AgentNoLongerMovesToItem;
    }
}
