using UnityEngine;

public interface IClickable
{
    /// <summary>
    /// Вызывается для обработки клика по объекту.
    /// </summary>
    /// <param name="o">Объект, по которому кликнули.</param>
    /// <param name="point">Точка клика.</param>
    void OnClick(GameObject obj, Vector3 point);
}
