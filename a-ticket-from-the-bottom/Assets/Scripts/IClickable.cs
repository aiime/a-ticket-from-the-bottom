using UnityEngine;

public interface IClickable
{
    /// <summary>
    /// Вызывается для обработки клика по объекту.
    /// </summary>
    void OnClick(GameObject clickedObject, Vector3 clickPoint);
}
