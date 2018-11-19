using UnityEngine;

public class ControllRaycaster : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Clickable")
                {
                    hit.transform.GetComponent<IClickable>().OnClick(hit.transform.gameObject, hit.point);
                }
            }
        }
    }
}
