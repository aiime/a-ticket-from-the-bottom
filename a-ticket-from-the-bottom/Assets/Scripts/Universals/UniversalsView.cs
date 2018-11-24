using UnityEngine;
using UnityEngine.UI;

public class UniversalsView : MonoBehaviour
{
    [SerializeField] UniversalsModel universalsModel;
    [SerializeField] Text universalsText;

    private void Start()
    {
        universalsModel.UniversalsChanged += UpdateUniversalView;
    }

    private void UpdateUniversalView(int newUnivarsalValue)
    {
        universalsText.text = newUnivarsalValue.ToString();
    }
}
