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
        //think about whether adding buttons for more specific things would be useful !!!
        if (GUILayout.Button("Default Values"))
        {
            Debug.Log("BUTTON PRESSED");

            //values
            m_script.Mass = 1f;
            m_script.Multiplier = 1f;
            m_script.CustomTimeStepValue = (float)1 / 60;

            //booleans
            m_script.Interpolate = true;
            m_script.CustomTimeStep = false;
            m_script.IsSwinging = true;
        }


        base.OnInspectorGUI();
       
    }
}
