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

[System.Serializable]
public class PendulumScriptEditor : Editor
{
    PendulumScript m_script;
    float a = 12;
    bool m_showInterpolateValue = false;
    bool m_swinging;
  
    private void OnEnable ()
    {
        m_script = (PendulumScript)target; 
    }


    public override void OnInspectorGUI()
    {
        this.serializedObject.Update();

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

        m_script.Interpolate = GUILayout.Toggle(m_script.Interpolate, "interpolate???");
        
        if(m_script.Interpolate)
        {
           /* EditorGUILayout.FloatField("float", m_script.TestFloat);*/
        }

      

        base.OnInspectorGUI();
        this.serializedObject.ApplyModifiedProperties();
    }
}
