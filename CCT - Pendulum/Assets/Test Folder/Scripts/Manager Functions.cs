using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class TestFunctions : MonoBehaviour
{

    bool m_ispaused = false;
    public UnityEvent m_startSwingEvent;
    public UnityEvent m_endSwingEvent;

    PendulumScript m_pendulumScript;
    public void OnQuitPressed(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            Application.Quit();
        }
    }

    public void OnPauseToggle(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            if(!m_ispaused)
            {
                Time.timeScale = 0;
                m_ispaused = true;
            }
            else
            {
                Time.timeScale = 1;
                m_ispaused = false;
            }
        }


    }

    public void OnClickPressed(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            m_startSwingEvent.Invoke();
        }

        if(_context.canceled)
        {
            m_endSwingEvent.Invoke();
        }



    }

}


