using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class AppliedForceTest : MonoBehaviour
{
    [SerializeField] Rigidbody m_body;
    [SerializeField] float m_customForce;

    private void Awake()
    {
        m_body = this.GetComponent<Rigidbody>();
    }

    public void AddCustomForce()
    {
        m_body.velocity = m_body.velocity +
            new Vector3(0,m_customForce, 0);
    }


    private void Update()
    {
        if (m_body != null)
        {
            AddCustomForce();
        }
    }

}
