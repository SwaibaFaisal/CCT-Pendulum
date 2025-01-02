using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System.Diagnostics.Tracing;

[CustomEditor(typeof(PendulumScript))]
[ExecuteInEditMode]
[CanEditMultipleObjects]
public class PendulumScriptEditor : Editor
{
    PendulumScript m_script;

    public override void OnInspectorGUI()
    {
        m_script = (PendulumScript)target;
        if (GUILayout.Button("Default Values"))
        {
            Debug.Log("BUTTON PRESSED");
            m_script.Multiplier = 1f;
            m_script.CustomTimeStepValue = (float)1 / 60;
        }


        base.OnInspectorGUI();
       
    }
}
