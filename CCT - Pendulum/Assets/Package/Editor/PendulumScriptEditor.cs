using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using Unity.VisualScripting;
using System.Diagnostics.Tracing;
using System.Drawing.Printing;

[CustomEditor(typeof(PendulumScript))]
[ExecuteInEditMode]
[CanEditMultipleObjects]
public class PendulumScriptEditor : Editor
{
    PendulumScript m_script;
    float a = 12;
    bool m_showInterpolateValue = false;
   

    public override void OnInspectorGUI()
    {
        m_script = (PendulumScript)target;      

        if (GUILayout.Button("Default Values"))
        {
            //values
            m_script.Mass = 1f;
            m_script.Multiplier = 1f;
            m_script.CustomTimeStepValue = (float)1 / 60;
            m_script.m_maxForce = 40f;
            m_script.m_minForce = -40f;
            m_script.Drag = 0.1f;

            //booleans
            m_script.Interpolate = true;
            m_script.CustomTimeStep = false;
            m_script.IsSwinging = true;
        }
        EditorGUILayout.LabelField("Minimum Force: ", m_script.m_minForce.ToString(), EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Maximum Force: ",m_script.m_maxForce.ToString(), EditorStyles.boldLabel);
       
        EditorGUILayout.MinMaxSlider(ref m_script.m_minForce, ref m_script.m_maxForce, -100f, 100f);
        base.OnInspectorGUI();

       

       
        m_showInterpolateValue = GUILayout.Toggle(m_showInterpolateValue, "interpolate?");

        if (m_showInterpolateValue)
        {
            EditorGUILayout.FloatField("Float test", a);
        }

    }
}
