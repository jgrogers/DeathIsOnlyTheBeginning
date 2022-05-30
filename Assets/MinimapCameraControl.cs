using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraControl : MonoBehaviour
{
    private Camera minimapCamera;
    private GameObject target = null;
    [SerializeField] float height = 100.0f;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Camera>().orthographicSize -= Input.mouseScrollDelta.y;
        if (target != null) {
            GetComponent<Camera>().enabled = true;
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y + height, target.transform.position.z);
            transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.forward);
        } else {
            GetComponent<Camera>().enabled = false;
        }
    }
    public void SetTarget(GameObject myTarget) {
        target = myTarget;
    }

}
