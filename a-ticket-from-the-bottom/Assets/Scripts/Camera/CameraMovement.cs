using UnityEngine;

namespace Ticket.Cam
{
    /// <summary>
    /// Двигает камеру за игроком со смещение <see cref="offset"/>. Вешать на камеру.
    /// </summary>
    [ExecuteInEditMode]
    [AddComponentMenu("Ticket/Camera/Camera movement")]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] Vector3 offset;

        private void LateUpdate()
        {
            transform.localPosition = playerTransform.position + offset;
        }
    }
}
