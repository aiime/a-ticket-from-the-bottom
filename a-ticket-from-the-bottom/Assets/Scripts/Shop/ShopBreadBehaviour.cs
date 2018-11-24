using UnityEngine;

public class ShopBreadBehaviour : MonoBehaviour
{
    [SerializeField] UniversalsModel universalsModel;

    public void BuyBread()
    {
        if (universalsModel.Universals >= 30)
        {
            universalsModel.Universals -= 30;
        }
    }
}
