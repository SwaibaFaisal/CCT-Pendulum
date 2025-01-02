using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    TextMeshProUGUI m_timerText;
    float m_timerTime;
    // Start is called before the first frame update
    private void Awake()
    {
        m_timerText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_timerTime += Time.deltaTime;
        m_timerText.text = m_timerTime.ToString();
        
    }

    void RestartTimer()
    {
        m_timerTime = 0;
    }
}
