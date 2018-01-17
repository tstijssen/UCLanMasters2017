using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGeneration : MonoBehaviour {

    public GameObject m_Target;     // reference to the target game object

    private Vector3 m_TargetPos;    // position and rotation of the target
    private Vector3 m_TargetRot;

	// Use this for initialization
	void Start () {
        MoveTarget();
        // instantiate target object once
        //m_Target = Instantiate(m_Target, m_TargetPos, Quaternion.Euler(m_TargetRot));   
    }

    void Update () {
        if (!m_Target.activeInHierarchy) // check if the target object is already active in the scene
        {
            // if not, move to a new location and reactivate
            MoveTarget();
            m_Target.gameObject.SetActive(true);
        }   
	}

    // move the target object to a new random position and rotation
    public void MoveTarget()
    {
        m_Target.transform.position = m_TargetPos = new Vector3(Random.Range(-20f, 20f), 2f, Random.Range(-20f, 20f));
        m_Target.transform.rotation = Quaternion.Euler(m_TargetRot = new Vector3(0f, Random.Range(0f, 259f), 0f));
    }
}
