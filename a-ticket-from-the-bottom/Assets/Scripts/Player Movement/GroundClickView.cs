using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Ticket.PlayerMovement
{
    /// <summary>
    /// Отвечает за отрисовку кружочка на земле в точке конца движения игрока.
    /// </summary>
    [AddComponentMenu("Ticket/Player movement/Ground click view")]
    public class GroundClickView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform circle;

        private bool blocked;
        private Vector3 currentAgentDestination;

        // Для отрисовки линий вдоль маршрута агента. Пока не используются.
        [SerializeField] private LineRenderer lineRenderer;
        private Coroutine drawLinesCoroutine;

        private void Update()
        {
            if (AgentStartedMovement())
            {
                DisplayCircleOnDestination(agent.destination);
                currentAgentDestination = agent.destination;
                blocked = true;
            }
            else if (AgentDestinationChanged())
            {
                HideCircle();
                blocked = false;
            }
            else if (AgentStoppedMovement())
            {
                HideCircle();
                blocked = false;
            }
        }

        private bool AgentStartedMovement()
        {
            return !blocked && agent.hasPath;
        }

        private bool AgentDestinationChanged()
        {
            return currentAgentDestination != agent.destination;
        }

        private bool AgentStoppedMovement()
        {
            return blocked && !agent.hasPath;
        }

        private void DisplayCircleOnDestination(Vector3 destination)
        {
            Vector3 circleNewPosition = new Vector3(destination.x, 0.1f, destination.z);
            circle.position = circleNewPosition;
        }

        private void HideCircle()
        {
            circle.position = new Vector3(0, -1, 0);
        }

        // Дальше методы для отрисовки линий вдоль маршрута агента. Пока не используются.
        private void StartDrawingLinesToDestination(Vector3 destination)
        {
            drawLinesCoroutine = StartCoroutine(DrawLinesToDestination(destination));
        }

        private void StopDrawingLinesToDestination()
        {
            if (drawLinesCoroutine != null)
            {
                StopCoroutine(drawLinesCoroutine);
                lineRenderer.enabled = false;
            }
        }

        IEnumerator DrawLinesToDestination(Vector3 destination)
        {
            lineRenderer.enabled = true;

            while (true)
            {
                lineRenderer.positionCount = agent.path.corners.Length;
                for (int i = 0; i < agent.path.corners.Length; i++)
                {
                    lineRenderer.SetPosition(i, agent.path.corners[i]);
                }
                yield return null;
            }
        }
    }
}
