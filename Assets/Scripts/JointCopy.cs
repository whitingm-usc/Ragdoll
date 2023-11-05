using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointCopy : MonoBehaviour
{
    public Transform m_copyXform;
    public Transform m_copyParent;

    ConfigurableJoint m_joint;
    Matrix4x4 m_initial;
    Transform m_parent;
    float m_origSpring;

    // Start is called before the first frame update
    void Start()
    {
        m_joint = GetComponent<ConfigurableJoint>();
        m_origSpring = m_joint.angularXDrive.positionSpring;
        m_parent = m_joint.connectedBody.transform;
        m_initial = m_copyXform.localToWorldMatrix;
        m_initial = m_copyParent.localToWorldMatrix.inverse * m_initial;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Matrix4x4 copyMat = m_copyXform.localToWorldMatrix;
        copyMat = m_copyParent.localToWorldMatrix.inverse * copyMat;

#if true    // directly apply the results to the transform - bypass the Rigidbody altogether
        Matrix4x4 tempMat = m_parent.localToWorldMatrix * copyMat;
        Vector3 pos = transform.position;
        Vector3 targetPos = tempMat.GetPosition();
        Quaternion rot = transform.rotation;
        Quaternion targetRot = tempMat.rotation;
        pos = Vector3.Lerp(pos, targetPos, JointHelp.Help);
        rot = Quaternion.Slerp(rot, targetRot, JointHelp.Help);
        transform.position = pos;
        transform.rotation = rot;
#endif

        copyMat = copyMat.inverse * m_initial;
        m_joint.targetRotation = copyMat.rotation;

#if false   // a slider to let me adjust the spring forces in realtime
        float spring = 100.0f * JointHelp.Help * m_origSpring;
        float damp = Mathf.Sqrt(2.0f * spring);
        {
            var xDrive = m_joint.angularXDrive;
            xDrive.positionSpring = spring;
            xDrive.positionDamper = damp;
            m_joint.angularXDrive = xDrive;
        }
        {
            var yzdrive = m_joint.angularYZDrive;
            yzdrive.positionSpring = spring;
            yzdrive.positionDamper = damp;
            m_joint.angularYZDrive = yzdrive;
        }
#endif
    }
}
