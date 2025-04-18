using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

public class TestPlayer : MonoBehaviour
{

    [SerializeField] PendulumScript m_script;
    [SerializeField] LayerMask m_targetLayer;
    
    [SerializeField] float m_speed;
    [SerializeField] float m_jumpForce;
    [Tooltip("1 = full control, 0 = no control")]
    [SerializeField] float m_airControl;
    float m_speedLocal;
    [SerializeField] float m_extraGravity;
    
    bool m_lockHorizontal = false;
    bool m_lockVertical = false;
    bool m_canJump;

    float m_horizontalMoveForce;
    float m_verticalMoveForce;

    [SerializeField] LayerMask m_worldObjectLayer;

    public void OnClickPressed(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            Vector3 _mousePosition = Input.mousePosition;
            
            Ray _ray = Camera.main.ScreenPointToRay(_mousePosition);

            if(Physics.Raycast(_ray, out RaycastHit _hitData, Mathf.Infinity, m_targetLayer))
            {
                if(_hitData.collider != null)
                {
                    m_script.StartSwing(_hitData.point);
                    m_lockHorizontal = true;
                    m_lockVertical = true;
                }
                
            }
            else
            {
                print("NO TARGET");
            }

        }    

        if(_context.canceled) 
        {
            m_script.EndSwing();
            m_lockHorizontal = false;
            m_lockVertical = false;
        }

    }

    public void OnPlayerMoveHorizontal(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            m_horizontalMoveForce = _context.ReadValue<float>() * m_speedLocal;
        }

        if (_context.canceled)
        {
            m_horizontalMoveForce = 0;
        }
    }

    public void OnPlayerMoveVertical(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            m_verticalMoveForce = _context.ReadValue<float>() * m_speedLocal;
        }

        if (_context.canceled)
        {
            m_verticalMoveForce = 0;
        }


    }

    public void OnJumpPressed(InputAction.CallbackContext _context)
    {
        if(_context.started && m_canJump)
        {
            Jump();
        }
    }

    public void Update()
    {
        m_script.RigidBody.velocity += new Vector3(m_horizontalMoveForce, 0, m_verticalMoveForce);

        if (Physics.Raycast(m_script.RigidBody.transform.position, Vector3.down, 0.8f, m_worldObjectLayer) || Physics.Raycast(m_script.RigidBody.transform.position, Vector3.down, 0.8f, m_targetLayer))
        {
            m_canJump = true;
        }
        else
        {
            m_canJump = false;

            if(!m_script.IsSwinging)
            {
                m_script.RigidBody.velocity -= new Vector3(0, m_extraGravity, 0); 
            }
        }

        if(m_script.IsSwinging)
        {
            m_speedLocal = m_speed * m_airControl;

        }
        else
        {
            m_speedLocal = m_speed;
        }

    }

    public void FixedUpdate()
    {
       
       
    }
    void Jump()
    {
        m_script.RigidBody.velocity += new Vector3(0, m_jumpForce, 0);
    }


}
