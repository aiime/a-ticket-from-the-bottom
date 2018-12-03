using UnityEngine;
using UnityEditor;
using Ticket.GeneralMovement;

[CustomEditor(typeof(MovementTarget))]
public class MovementTargetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MovementTarget movementTarget = (MovementTarget)target;
        
        if (movementTarget.hasAlternativeDestination)
        {
            movementTarget.alternativeDestination = 
                (Transform)EditorGUILayout.ObjectField("Alternative Destination", 
                                                       movementTarget.alternativeDestination, 
                                                       typeof(Transform), true);
        }
    }
}
