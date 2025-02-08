using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PendulumDebugging : MonoBehaviour
{
    PendulumScript m_script;
    LineRenderer m_lineRenderer;
    [SerializeField] bool m_showDebugLine;
    [SerializeField] float m_debugLineWidth = 0.1f;
     
    private void Awake()
    {
        m_script = this.GetComponent<PendulumScript>();
        m_lineRenderer = this.GetComponent<LineRenderer>();
        SetVariables();
    }

    public void Update()
    {
        if(m_showDebugLine)
        {
            DrawDebugLines();
        } 
    }

    void SetVariables()
    {
        m_lineRenderer.SetWidth(m_debugLineWidth, m_debugLineWidth);
    }
    public void DrawDebugLines()
    {
        Vector3 _startPoint = this.transform.position;
        Vector3 _endPosition = m_script.TargetTransform.position;

        m_lineRenderer.SetPosition(0, _startPoint);
        m_lineRenderer.SetPosition(1, _endPosition);
    }

}
