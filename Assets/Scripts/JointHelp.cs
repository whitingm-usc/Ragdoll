using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JointHelp : MonoBehaviour
{
    static float s_help = 1.0f;
    static JointHelp s_theSlider;

    Slider m_slider;

    public static float Help
    {
        get { return s_help; }
        set
        {
            s_help = value;
            if (null != s_theSlider && null != s_theSlider.m_slider)
                s_theSlider.m_slider.value = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (null != s_theSlider)
        {
            Destroy(gameObject);
            return;
        }
        s_theSlider = this;
        m_slider = gameObject.GetComponent<Slider>();
        if (null != m_slider)
        {
            m_slider.onValueChanged.AddListener(OnValueChanged);
            s_help = m_slider.value;
        }
    }

    void OnValueChanged(float value)
    {
        s_help = value;
    }

}
