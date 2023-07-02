using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        cameraForward,
        cameraForwardInverted,
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                break;
            case Mode.cameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.cameraForwardInverted:
                transform.forward -= Camera.main.transform.forward;
                break;  
        }

        
    }

}
