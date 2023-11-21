using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Management;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    [SerializeField] private InputActionReference switchKeyReference;
    public MonoBehaviour movementScript;

    private bool isUsingThirdPerson;
    private bool canSwitchCamera = true;
    private void Start()
    {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
        isUsingThirdPerson = true;
    }

    private void Update()
    {
        float floatSwitchKeyValue = switchKeyReference.action.ReadValue<float>();
        bool booleanSwitchKeyValue = Convert.ToBoolean(floatSwitchKeyValue > 0.5f);
        
        if (booleanSwitchKeyValue && canSwitchCamera)
        {
            StartCoroutine(SwitchCameraWithDelay());
        }
    }
    private IEnumerator SwitchCameraWithDelay()
    {
        canSwitchCamera = false;

    if (isUsingThirdPerson)
    {
        thirdPersonCamera.SetActive(false);
        firstPersonCamera.SetActive(true);
        movementScript.enabled = false;
    }
    else
    {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
        movementScript.enabled = true;
    }

    isUsingThirdPerson = !isUsingThirdPerson;

    yield return new WaitForSeconds(1f);

    canSwitchCamera = true;
    }
}