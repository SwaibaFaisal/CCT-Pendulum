using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomPhysicsBase : MonoBehaviour
{
    protected Rigidbody m_rigidBody;
    [SerializeField] float m_mass;
    [SerializeField] float m_drag;
    [SerializeField] bool m_interpolate;
    [SerializeField] bool m_customTimeStep;
    [Tooltip("Fixed Delta Time value. Default value is set to 1/50, reccomended value is 1/60")]
    [SerializeField][Min(1 / 60f)] float m_customTimeStepValue;

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
        if (m_customTimeStep)
        {
            Time.fixedDeltaTime = m_customTimeStepValue;
        }
    }

    
    public virtual void SetForce(Vector3 _force, ForceMode _forceMode)
    {
        m_rigidBody.AddForce(_force, _forceMode);
    }

    #region getters and setters
    public float Mass { get { return m_mass; } set {  m_mass = value; }}
    public bool Interpolate { get { return m_interpolate; } set { m_interpolate = value; }}
    public float CustomTimeStepValue { get { return m_customTimeStepValue; } set { m_customTimeStepValue = value; }}
    public bool CustomTimeStep { get { return m_customTimeStep; } set { m_customTimeStep = value; }}

    public float Drag { get { return m_drag; } set { m_drag = value; }}

    #endregion
}
