using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOut : MonoBehaviour
{
    public float m_timeOut = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown(m_timeOut));
    }

    IEnumerator CountDown(float timeOut)
    {
        yield return new WaitForSeconds(timeOut);
        Destroy(gameObject);
    }
}
