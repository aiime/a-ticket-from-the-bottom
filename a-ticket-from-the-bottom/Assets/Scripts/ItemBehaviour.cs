using UnityEngine;
using Ticket.Items;
using Ticket.Inventory;

public class ItemBehaviour : MonoBehaviour, IClickable
{
    public Mover mover;
    public InventoryModel inventory;
    public Transform playerTransform;
    public Item item;

    private bool agentMovesToItem;

    public void OnClick(GameObject clickedObject, Vector3 clickPoint)
    {
        mover.MoveTo(clickedObject.transform.position);

        agentMovesToItem = true;
        mover.MovementEnd += AgentNoLongerMovesToItem;
    }

    private void AgentNoLongerMovesToItem()
    {
        agentMovesToItem = false;

        mover.MovementEnd -= AgentNoLongerMovesToItem;
    }

    /* Используется OnTriggerStay, а не OnTriggerEnter т.к. если предмет заспаунится прямо на герое, 
       то OnTriggerEnter при щелчке по предмету не сработает. */
    private void OnTriggerStay(Collider other)
    {
        if (agentMovesToItem && other.gameObject.tag == "Player")
        {
            agentMovesToItem = false;

            mover.Stop();
            playerTransform.rotation = 
                Quaternion.LookRotation(this.transform.position - playerTransform.position);
            inventory.AddItem(item);
            Destroy(transform.gameObject);
        }
    }

    private void OnDestroy()
    {
        mover.MovementEnd -= AgentNoLongerMovesToItem;
    }
}
