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
            m_script.CustomTimeStepValue = 0.001f;
            m_script.m_maxForce = 100f;
            m_script.m_minForce = -100f;
            m_script.Drag = 0.001f;
            m_script.JumpForce = new Vector3(4,6,4);
            m_script.SwingSpeed = 1f;

            //booleans
            m_script.Interpolate = true;
            m_script.UseCustomTimeStep = false;
            m_script.IsSwinging = false;
        }


        EditorGUILayout.LabelField("Minimum Force: ", m_script.m_minForce.ToString(), EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Maximum Force: ",m_script.m_maxForce.ToString(), EditorStyles.boldLabel);       
        EditorGUILayout.MinMaxSlider(ref m_script.m_minForce, ref m_script.m_maxForce, -200f, 200f);


        base.OnInspectorGUI();
        this.serializedObject.ApplyModifiedProperties();
    }
}
