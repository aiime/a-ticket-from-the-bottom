using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleClickBehaviour : MonoBehaviour, IClickable
{
    [SerializeField] private Mover mover;

    private void Start()
    {
        
    }

    public void OnClick(GameObject obj, Vector3 point)
    {
        mover.Move(obj.transform.position);
    }


}
