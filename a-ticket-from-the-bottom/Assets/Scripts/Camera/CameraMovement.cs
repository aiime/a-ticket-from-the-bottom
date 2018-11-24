using UnityEngine;

[ExecuteInEditMode]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform heroTransform;
    [SerializeField] Vector3 offset;

    private void LateUpdate()
    {
        transform.position = heroTransform.position + offset;
    }
}
