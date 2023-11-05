using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject m_bulletPrefab;
    public float m_velocity = 100.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject bullet = Instantiate(m_bulletPrefab, Camera.main.transform.position, Quaternion.identity);
            if (null != bullet)
            {
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (null != rb)
                {
                    rb.velocity = ray.direction * m_velocity;
                }
            }
        }
        
    }
}
