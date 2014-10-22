using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Event))]
public class EventEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        Event ed = (Event)target;
        ed.CodeWindow = GUILayout.TextArea(ed.CodeWindow);
    }
}