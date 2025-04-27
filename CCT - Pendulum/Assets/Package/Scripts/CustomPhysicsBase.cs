using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomPhysicsBase : MonoBehaviour
{
    protected Rigidbody m_rigidBody;

    [Header("RigidBody Component References")]

    [SerializeField] float m_mass;
    [SerializeField] float m_drag;
    [Tooltip("Smoothing effects of running physics between frames -> Overrides Rigidbody value ")]
    [SerializeField] bool m_interpolate;


    [SerializeField] bool m_useCustomTimeStep;
    [Tooltip("Fixed Delta Time value. The lower the number, the more times the FixedUpdate loop runs in a second")]
    [SerializeField] [Min(0.001f)] float m_customTimeStepValue;


    public virtual void Awake()
    {
        m_rigidBody = this.GetComponent<Rigidbody>();
    }

    public virtual void SetVariables()
    {
        m_rigidBody.mass = m_mass;
        m_rigidBody.drag = m_drag;
        if(m_interpolate) 
        {
            m_rigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        }
        if (m_useCustomTimeStep)
        {
            Time.fixedDeltaTime = m_customTimeStepValue;
        }
    }

    
  

    #region getters and setters
    public float Mass { get { return m_mass; } set {  m_mass = value; }}
    public bool Interpolate { get { return m_interpolate; } set { m_interpolate = value; }}
    public float CustomTimeStepValue { get { return m_customTimeStepValue; } set { m_customTimeStepValue = value; }}
    public bool UseCustomTimeStep { get { return m_useCustomTimeStep; } set { m_useCustomTimeStep = value; }}
    public float Drag { get { return m_drag; } set { m_drag = value; }}



    #endregion
}
