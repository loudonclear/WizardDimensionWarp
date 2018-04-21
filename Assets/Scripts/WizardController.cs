using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour {

    public Vector3 wizardOffset;
    public Transform cam;

    private void LateUpdate()
    {
        transform.position = new Vector3(0, 0, cam.position.z) + wizardOffset;
    }
}
