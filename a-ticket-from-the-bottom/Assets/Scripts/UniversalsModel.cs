using System;
using UnityEngine;

/// <summary>
/// Через этот скрипт устанавливается количество универсалов, которыми обладает игрок, а также извлекается
/// их текущее значение.
/// </summary>
public class UniversalsModel : MonoBehaviour
{
    public int Universals
    {
        get
        {
            return universals;
        }

        set
        {
            if (value < 0)
            {
                int removedAmount = universals;
                universals = 0;

                if (UniversalsRemoved != null) UniversalsRemoved.Invoke(removedAmount);
            }

            else if (value > 999)
            {
                int addedAmount = 999 - universals;
                universals = 999;

                if (UniversalsAdded != null) UniversalsAdded.Invoke(addedAmount);
            }

            else if (value > universals)
            {
                int addedAmount = value - universals;
                universals = value;

                if (UniversalsAdded != null) UniversalsAdded.Invoke(addedAmount);
            }

            else if (value < universals)
            {
                int removedAmount = universals - value;
                universals = value;

                if (UniversalsRemoved != null) UniversalsRemoved.Invoke(removedAmount);
            }

            if (UniversalsChanged != null) UniversalsChanged.Invoke(universals);
        }
    }

    public Action<int> UniversalsAdded;
    public Action<int> UniversalsRemoved;
    public Action<int> UniversalsChanged;

    private int universals;
}
