using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumDebugging : MonoBehaviour
{
    Rigidbody m_rb;
    PendulumScript m_script;

    private void Awake()
    {
        m_script = this.GetComponent<PendulumScript>();
    }

    public void Update()
    {
    

     Debug.DrawLine(this.transform.position, m_script.TargetTransform.position, Color.red);
        

    }

}
