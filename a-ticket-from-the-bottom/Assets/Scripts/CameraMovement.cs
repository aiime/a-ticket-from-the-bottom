using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform heroTransform;
    private Vector3 heroCurrentPosition;
    private Vector3 heroPreviousPosition;
    private Vector3 heroDirection;

    private void LateUpdate()
    {
        heroCurrentPosition = heroTransform.position;
        heroDirection = heroCurrentPosition - heroPreviousPosition;
        transform.Translate(heroDirection, Space.World);
        heroPreviousPosition = heroTransform.position;
    }
}
