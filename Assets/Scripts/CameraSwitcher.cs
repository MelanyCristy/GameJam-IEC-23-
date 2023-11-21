using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Movement newMovementScript;
    [SerializeField] private InputActionReference thidSwitchKeyReference;
    [SerializeField] private InputActionReference firstPersonSwitchKeyReference;
    public float switchDelay = 0.2f;
    private float lastSwitchTime;

    void Start()
    {
        // Assuming you have a reference to the Movement script
        if (newMovementScript == null)
        {
            Debug.LogError("Movement script reference not set.");
        }
    }

    void Update()
    {
        float floatthidSwitchKeyValue = thidSwitchKeyReference.action.ReadValue<float>();
        float floatFirstPersonSwitchKeyValue = firstPersonSwitchKeyReference.action.ReadValue<float>();
        if (floatthidSwitchKeyValue > 0.5f && Time.time - lastSwitchTime > switchDelay)
        {
            SwitchToNextCamera();
            //SwitchCamera();
            lastSwitchTime = Time.time;
        }
        if (floatFirstPersonSwitchKeyValue > 0.5f && Time.time - lastSwitchTime > switchDelay)
        {
            ToggleFirstPersonCamera();
            lastSwitchTime = Time.time;
        }
        
    }
    void SwitchToNextCamera()
    {
        // Call the SwitchCamera method on the Movement script
        int nextCameraIndex = (newMovementScript.activeCameraIndex + 1) % newMovementScript.cameras.Length;
        newMovementScript.SwitchCamera(nextCameraIndex);
    }
   void ToggleFirstPersonCamera()
{
    // Toggle between the first-person camera and the current array camera
    if (newMovementScript.IsUsingFirstPersonCamera())
    {
        // Switch to the next camera in the array
        int nextCameraIndex = (newMovementScript.activeCameraIndex + 1) % newMovementScript.cameras.Length;
        newMovementScript.SwitchCamera(nextCameraIndex);
    }
    else
    {
        // Switch to the first-person camera
        int firstPersonCameraIndex = GetFirstPersonCameraIndex();

        // Check if the first-person camera index is valid
        if (firstPersonCameraIndex >= 0 && firstPersonCameraIndex < newMovementScript.cameras.Length)
        {
            newMovementScript.SwitchCamera(firstPersonCameraIndex);
        }
    }
}
    int GetFirstPersonCameraIndex()
    {
        // Find the index of the first-person camera in the array
        for (int i = 0; i < newMovementScript.cameras.Length; i++)
        {
            if (newMovementScript.cameras[i].name == "FirstPersonCamera")
            {
                return i;
            }
        }
        // If not found, return -1
        return -1;
    }
}
