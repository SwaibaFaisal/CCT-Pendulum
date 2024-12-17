using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomPhysicsBase : MonoBehaviour
{
    Rigidbody m_rigidBody;
    [SerializeField] float m_mass;
    [SerializeField] float m_GravityScale;

    void Awake()
    {
        m_rigidBody = this.GetComponent<Rigidbody>();
    }

    public virtual void SetVariables()
    {
        m_rigidBody.mass = m_mass;
    }

    public virtual Vector3 CalculateForce()
    {
        Vector3 _force = Vector3.zero;
        return _force;
    }
    
    public virtual void SetForce(Vector3 _force, ForceMode _forceMode)
    {
        m_rigidBody.AddForce(_force, _forceMode);
    }

}
