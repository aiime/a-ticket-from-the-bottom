using System;
using UnityEngine;

namespace Ticket.GeneralMovement
{
    [AddComponentMenu("Ticket/General Movement/Movement target")]
    public class MovementTarget : MonoBehaviour
    {
        [SerializeField] Mover mover;
        [SerializeField] Transform aiTransform;

        public bool hasAlternativeDestination;
        [HideInInspector] public Transform alternativeDestination;

        public Action TargetReached;

        bool subscribed;

        public void WaitForArrival()
        {
            if (!subscribed)
            {
                mover.WentToDestination += StartWaiting;
                subscribed = true;
            }       
        }

        void StartWaiting()
        {
            mover.ReachedDestination += RotateToTarget;
            mover.ChangedDestination += StopWaiting;
        }

        void StopWaiting()
        {
            mover.WentToDestination -= StartWaiting;
            mover.ReachedDestination -= RotateToTarget;
            mover.ChangedDestination -= StopWaiting;
            subscribed = false;
        }

        void RotateToTarget()
        {
            StopWaiting();
            Quaternion rotation = Quaternion.LookRotation(transform.position - aiTransform.position);
            Quaternion rotationFreezeXY = new Quaternion(0, rotation.y, 0, rotation.w);
            aiTransform.rotation = rotationFreezeXY;
            if (TargetReached != null) TargetReached.Invoke();
        }
    }
}
