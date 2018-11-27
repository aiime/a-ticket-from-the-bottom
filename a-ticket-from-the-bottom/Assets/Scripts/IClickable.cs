using UnityEngine;

/// <summary>
/// Вешается на объекты, которые должен регестрировать ControllRaycaster.
/// </summary>
public interface IClickable
{
    /// <summary>
    /// Вызывается для обработки клика по объекту.
    /// </summary>
    void OnClick(GameObject clickedObject, Vector3 clickPoint);
}
