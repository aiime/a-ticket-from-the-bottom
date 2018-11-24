using System.Collections;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField] Mover npcMover;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float restTime;

    private int i;

    private void Start()
    {
        npcMover.MoveTo(patrolPoints[0].position);
        npcMover.TargetReached += () =>
        {
            i = (i + 1) % patrolPoints.Length;
            StartCoroutine(RestThenMove(patrolPoints[i]));
        };
    }

    IEnumerator RestThenMove(Transform nextPoint)
    {
        yield return new WaitForSeconds(restTime);
        npcMover.MoveTo(nextPoint.position);
    }
}
