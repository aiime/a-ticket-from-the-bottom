using UnityEngine;
using Pathfinding;
using Ticket.GeneralMovement;
using UnityEngine.EventSystems;

namespace Ticket.Visual
{
    /// <summary>
    /// Рисует линию и конечные/начальные метки на земле от игрока до точки назначения.
    /// </summary>
    [AddComponentMenu("Ticket/Visual/Path Renderer")]
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(LineRenderer))]
    public class PathRenderer : MonoBehaviour
    {
        [SerializeField] Material onTheWay;
        [SerializeField] Material selection;
        [SerializeField] Transform pathStartPointer;
        [SerializeField] Transform pathEndPointer;
        [SerializeField] Transform pointer;

        AIPath playerAI;
        Seeker playerSeeker;
        Mover playerMover;
        LineRenderer lineRenderer;
        Renderer pathStartRenderer;
        Renderer pathEndRenderer;
        bool hitGround;
        bool waitForRender;

        void Awake()
        {
            playerAI = GetComponent<AIPath>();
            playerSeeker = GetComponent<Seeker>();
            playerMover = GetComponent<Mover>();
            lineRenderer = GetComponent<LineRenderer>();

            pathStartRenderer = pathStartPointer.GetComponent<Renderer>();
            pathEndRenderer = pathEndPointer.GetComponent<Renderer>();

            playerMover.WentToDestination += SetOnTheWayRender;
            playerMover.ReachedDestination += OffOnTheWayRender;
            playerSeeker.pathCallback += RenderPath;
        }

        //MovementTarget movementTarget;
        void Update()
        {
            if (Input.GetMouseButton(1) && !playerAI.canMove && !waitForRender)
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                RaycastHit hit = RaycastGround();

                if (hit.transform.tag == "Ground")
                {
                    waitForRender = true;
                    pointer.gameObject.SetActive(false);
                    playerAI.destination = hit.point;
                    playerAI.SearchPath();
                    // Дальше, как только ИИ вычислит путь, сработает 'pathCallback', и запустится подписанный на него 
                    // метод 'RenderPath', который отрисует путь.
                }

                if (hit.transform.tag == "Movement Target")
                {
                    waitForRender = true;
                    pointer.gameObject.SetActive(false);

                    MovementTarget movementTarget = hit.transform.GetComponent<MovementTarget>();
                    if (movementTarget.hasAlternativeDestination)
                    {
                        playerAI.destination = movementTarget.alternativeDestination.position;
                        playerAI.SearchPath();
                    }
                    else
                    {
                        playerAI.destination = hit.transform.position;
                        playerAI.SearchPath();
                    }
 
                    // Дальше, как только ИИ вычислит путь, сработает 'pathCallback', и запустится подписанный на него 
                    // метод 'RenderPath', который отрисует путь.
                }
            }

            if (!Input.GetMouseButton(1))
            {
                pointer.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Возвращает <see cref="RaycastHit"/> по месту попадания. Если попал по земле, то поднимет флаг 
        /// <see cref="hitGround"/>, а иначе опустит.
        /// </summary>
        RaycastHit RaycastGround()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            Physics.Raycast(ray, out hit);

            return hit;
        }

        void SetOnTheWayRender()
        {
            lineRenderer.material = onTheWay;
            pathStartRenderer.material = onTheWay;
            pathEndRenderer.material = onTheWay;
        }

        /// <summary>
        /// Установит материалы линии и конечных/начальных меток в значения по умолчанию <see cref="selection"/>, а также
        /// выключит их.
        /// </summary>
        void OffOnTheWayRender()
        {
            lineRenderer.material = selection;
            pathStartRenderer.material = selection;
            pathEndRenderer.material = selection;

            lineRenderer.positionCount = 0;
            pathStartPointer.gameObject.SetActive(false);
            pathEndPointer.gameObject.SetActive(false);
        }

        void RenderPath(Path p)
        {
            Vector3 startPosV3 = (Vector3)p.path[0].position;
            Vector3 endPosV3 = (Vector3)p.path[p.path.Count - 1].position;

            pathStartPointer.position = new Vector3(startPosV3.x, pathStartPointer.position.y, startPosV3.z);
            pathEndPointer.position = new Vector3(endPosV3.x, pathEndPointer.position.y, endPosV3.z);

            pathStartPointer.gameObject.SetActive(true);
            pathEndPointer.gameObject.SetActive(true);

            lineRenderer.positionCount = p.path.Count;

            for (int i = 0; i < p.path.Count; i++)
            {
                Vector3 pathPosV3 = (Vector3)p.path[i].position;
                lineRenderer.SetPosition(i, new Vector3(pathPosV3.x, pathEndPointer.position.y, pathPosV3.z));
            }

            waitForRender = false;
        }
    }
}
