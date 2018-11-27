using UnityEngine;

namespace Ticket.Camera
{
    /// <summary>
    /// Двигает камеру за игроком. Вешать на камеру.
    /// </summary>
    [ExecuteInEditMode]
    [AddComponentMenu("Ticket/Camera/Camera movement")]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] Vector3 offset;

        private void LateUpdate()
        {
            transform.position = playerTransform.position + offset;
        }
    }
}
