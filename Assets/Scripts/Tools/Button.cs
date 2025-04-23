using UnityEditor;
using UnityEngine;

public class Button : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Button"))
        {

        }
    }
}
