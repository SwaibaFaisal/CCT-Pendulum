using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class PendulumScript : CustomPhysicsBase
{
    [SerializeField] Transform m_targetTransform;


    #region inspector variables

    [Header("Values")]
    [SerializeField] float m_multiplier;
    [SerializeField] bool m_isSwinging;
 

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
                SetForce(CalculateForceDirection() * CalculateNetForce(), ForceMode.Force);
            }
        }
        
    }

    public override void SetVariables()
    {
        base.SetVariables();
        m_startPosition = this.transform.position;
        m_ropeLength = Vector3.Distance( m_targetTransform.position, this.transform.position);
    }

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


    #region getters and setters

    public float Multiplier { get { return m_multiplier; } set { m_multiplier = value; }}


    public bool isSwinging
    { get { return m_isSwinging; } set { m_isSwinging = value;}}
    #endregion

}
