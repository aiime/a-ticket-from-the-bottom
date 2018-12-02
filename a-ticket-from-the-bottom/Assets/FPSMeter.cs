using UnityEngine;

public class FPSMeter : MonoBehaviour
{
    [SerializeField] bool showFPS;

    GUIStyle guiStyle;

    void Awake()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 32;
    }

    void OnGUI()
    {
        if (showFPS)
        {
            GUI.Label(new Rect(0, 0, 200, 200), (1.0f / Time.deltaTime).ToString(), guiStyle);
        }  
    }
}
