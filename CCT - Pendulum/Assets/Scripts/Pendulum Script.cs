using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PendulumScript : CustomPhysicsBase
{
    [SerializeField] Transform m_targetTransform;

    #region inspector variables
    [SerializeField] float m_angleBetweenPoints;
    [SerializeField] float m_timeMultiplier;
    [SerializeField] float m_multiplier;


    #endregion

    #region private variables
    float m_ropeLength;
    Vector3 m_startPosition;
    Vector3 m_directionFacingPivot;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
    }
    

   // Update is called once per frame
    void Update()
    {
        DrawDebugLines();
        float m_frameRateMultiplier = m_timeMultiplier * Time.deltaTime;

        if(Vector3.Distance(m_targetTransform.position, this.transform.position) >= m_ropeLength)
        {
            SetForce( (CalculateForceDirection() * CalculateNetForce() * m_multiplier ) * m_frameRateMultiplier, ForceMode.Force);
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

    void DrawDebugLines()
    {
        Debug.DrawLine(this.gameObject.transform.position, m_targetTransform.position, Color.blue);
        Debug.DrawLine(Vector3.zero, Vector3.up * 50, Color.red);

    }
}
