using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PendulumScript : CustomPhysicsBase
{
    [SerializeField] float test;
    [SerializeField] Transform targetTransform;

    #region variables
    [SerializeField] float angleBetweenPoints;
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

   // Update is called once per frame
    void Update()
    {
        DrawLineBetweenPoints();
    }

    void CalculateAcuteAngle()
    {
        //calculates angle based on equation
        Vector3 _targetPosition = targetTransform.position;
        

    }

    void CalculateLineEquation()
    {
        float angle = Vector3.Angle(this.transform.position - targetTransform.position, Physics.gravity.normalized);
        print(angle);
    }

    void DrawLineBetweenPoints()
    {
        Debug.DrawLine(this.gameObject.transform.position, targetTransform.position, Color.blue);
        Debug.DrawLine(Vector3.zero, Vector3.up * 50, Color.red);
        CalculateLineEquation();
    }
}
