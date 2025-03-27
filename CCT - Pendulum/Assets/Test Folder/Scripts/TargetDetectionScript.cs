using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetDetectionScript : MonoBehaviour
{
    [SerializeField] PendulumScript m_script;
    [SerializeField] LayerMask m_hittableLayer;
    

    public void OnClickPressed(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            Vector3 _mousePosition = Input.mousePosition;
            
            Ray _ray = Camera.main.ScreenPointToRay(_mousePosition);
            if(Physics.Raycast(_ray, out RaycastHit _hitData, Mathf.Infinity, m_hittableLayer))
            {
                if(_hitData.collider != null)
                {
                    m_script.StartSwing(_hitData.point);
                    
                }
            }

        }    

        if(_context.canceled) 
        {
            m_script.EndSwing();
        }

    }


}
