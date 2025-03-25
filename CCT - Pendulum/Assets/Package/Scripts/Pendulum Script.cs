using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(PendulumDebugging))]

public class PendulumScript : CustomPhysicsBase
{
    [Header("Pivot Transform")]
    [SerializeField] Transform m_targetTransform;
    Transform m_newTargetTransform;


    #region inspector variables

    [Header("Values")]
    /*[SerializeField]*/ float m_multiplier;
    
    [SerializeField] bool m_isSwinging;
    [SerializeField] [HideInInspector] float m_testFloat;
    // min max values, made public so they can be passed by reference
    [HideInInspector] public float m_maxForce;
    [HideInInspector] public float m_minForce;

    #endregion

    #region private variables
    float m_ropeLength;
    Vector3 m_startPosition;
    Vector3 m_directionFacingPivot;
    #endregion

    

    public override void Awake()
    {
        base.Awake();
        SetVariables();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       /* float m_frameRateMultiplier = m_timeMultiplier * Time.deltaTime;*/
        if(m_isSwinging)
        {
            //if the object is at the end of the "rope"
            if(Vector3.Distance(m_targetTransform.position, this.transform.position) >= m_ropeLength)
            {
                //clamp force
                Vector3 _force =  (Mathf.Clamp(CalculateNetForce(), m_minForce, m_maxForce)) * CalculateForceDirection();

              
                SetForce(_force, ForceMode.Force);
            }
        }
        
    }

    public override void SetVariables()
    {
        base.SetVariables();
        StartSwing();
    }

    public void CheckForNewTarget()
    {
        
    }

    public void SetTargetVariables()
    {
        m_startPosition = this.transform.position;
        m_ropeLength = Vector3.Distance(m_targetTransform.position, this.transform.position);
    }


    public void StartSwing()
    {
        m_isSwinging = true;
        SetTargetVariables();
    }

    public void EndSwing()
    {
        m_isSwinging = false;
    }

    #region math functions
    float CalculateAngle()
    {
        float _angle = Vector3.Angle(this.transform.position - m_targetTransform.position, Physics.gravity.normalized);
        return Mathf.Deg2Rad * _angle;
    }

    float CalculateCentripetalForce()
    {
        float a = Mathf.Pow( (m_rigidBody.mass * m_rigidBody.velocity.magnitude), 2);
        float b = a / m_ropeLength;
        return b;
    }

    float CalculateTensionForce()
    {
        float a = m_rigidBody.mass * Physics.gravity.magnitude;
        float b = Mathf.Cos(CalculateAngle());
        float c = a * b;
        return c;
    }

    float CalculateNetForce()
    {
        float a = CalculateTensionForce() + CalculateCentripetalForce();
        return a;
    }

    Vector3 CalculateForceDirection()
    {
        Vector3 a = (m_targetTransform.position - this.transform.position).normalized;
        return a;
    }

   
    #endregion 

    #region getters and setters
    public float Multiplier { get { return m_multiplier; } set { m_multiplier = value; }}
    public Transform TargetTransform { get { return m_targetTransform; } set { m_targetTransform = value; }}

    public Transform NewTargetTransform { get { return m_newTargetTransform; } set { m_newTargetTransform = value; }}
    public bool IsSwinging { get { return m_isSwinging; } set { m_isSwinging = value;}}

    public float TestFloat { get { return m_testFloat; } set { m_testFloat = value; }}

    /*public float MaxForce { get { return m_maxForce; } set { m_maxForce = value; }}*/
    #endregion

}
