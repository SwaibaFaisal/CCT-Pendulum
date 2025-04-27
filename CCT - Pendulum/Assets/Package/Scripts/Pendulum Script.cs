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
    Vector3 m_currentTargetPoint;

    #region inspector variables

    [Header("Values")]
    
    [SerializeField] bool m_isSwinging;
    [SerializeField] [HideInInspector] float m_testFloat;
    [SerializeField] Vector3 m_startJumpForce;
    [SerializeField] [Range(0,10)] float m_swingSpeed;

    // min max values, made public so they can be passed by reference for the editor script
    [HideInInspector] public float m_maxForce;
    [HideInInspector] public float m_minForce;

    #endregion

    #region private variables
    float m_ropeLength;
    Vector3 m_startPosition;
    #endregion

    public override void Awake()
    {
        base.Awake();
        SetVariables();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_isSwinging)
        {
            //if the object is at the end of the "rope"
            if(Vector3.Distance(m_currentTargetPoint, this.transform.position) >= m_ropeLength)
            {
                m_rigidBody.AddForce(CalculateNetForce(), ForceMode.Force);


            }
        }
    }

    public override void SetVariables()
    {
        base.SetVariables();
        
    }
    public void SetTargetVariables(Vector3 _targetPosition)
    {
        m_startPosition = this.transform.position;
        m_ropeLength = Vector3.Distance(_targetPosition, this.transform.position);
    }

    public void ClearTargetVariables()
    {
        m_startPosition = Vector2.zero;
        m_ropeLength = 0f;
        m_targetTransform = null;
    }

    public void StartSwing(Vector3 _targetPosition)
    {
        m_currentTargetPoint = _targetPosition;
        SetTargetVariables(_targetPosition);
        m_isSwinging = true;
        Jump();
    }

    public void EndSwing()
    {
        m_isSwinging = false;
        /*ClearTargetVariables();*/
        
       
    }

    public void Jump()
    {

        Vector3 b = new Vector3(
            CalculateForceDirection().x * m_startJumpForce.x,
                CalculateForceDirection().y * m_startJumpForce.y,
                CalculateForceDirection().z * m_startJumpForce.z);

        m_rigidBody.velocity += b;


    }

    #region math functions
    float CalculateAngle()
    {
        float _angle = Vector3.Angle(this.transform.position - m_currentTargetPoint, Physics.gravity.normalized);
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

    Vector3 CalculateNetForce()
    {
        float _initialForce = CalculateTensionForce() + CalculateCentripetalForce();
        //clamp force
        float _clampedForce = (Mathf.Clamp(_initialForce, m_minForce, m_maxForce));

        //apply direction 

        Vector3 _forceWithDirection = _clampedForce * CalculateForceDirection();

        //apply swing speed

        Vector3 _forceFinal = _forceWithDirection * m_swingSpeed;

        return _forceFinal ;
    }

   

    Vector3 CalculateForceDirection()
    {
        Vector3 a = (m_currentTargetPoint - this.transform.position).normalized;
        return a;
    }

   
    #endregion 

    #region getters and setters

    public Transform TargetTransform { get { return m_targetTransform; } set { m_targetTransform = value; }}

    public Vector3 CurrentTargetPoint { get { return m_currentTargetPoint; } set { m_currentTargetPoint = value; }} 
    public bool IsSwinging { get { return m_isSwinging; } set { m_isSwinging = value;}}

    public float TestFloat { get { return m_testFloat; } set { m_testFloat = value; }}

    public Vector3 JumpForce { get { return m_startJumpForce; } set { m_startJumpForce = value; } }

    public Rigidbody RigidBody { get { return m_rigidBody; } set { m_rigidBody = value; }}

    public float SwingSpeed { get { return m_swingSpeed; } set { m_swingSpeed = value; }}

    /*public float MaxForce { get { return m_maxForce; } set { m_maxForce = value; }}*/
    #endregion

}
