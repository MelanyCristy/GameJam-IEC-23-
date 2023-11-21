using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public InputActionReference moveAction;
    public float speed = 6.0f;
    public float rotationSpeed = 700.0f;

    // Array to hold your four cameras
    public Transform[] cameras;
    public int activeCameraIndex = 0;

    private Vector2 inputVector;

    void OnEnable()
    {
        moveAction.action.performed += OnMovePerformed;
        moveAction.action.canceled += OnMoveCanceled;
    }

    void OnDisable()
    {
        moveAction.action.performed -= OnMovePerformed;
        moveAction.action.canceled -= OnMoveCanceled;
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        inputVector = Vector2.zero;
    }

    void Update()
    {
         // Check if the first-person camera is active
        bool isFirstPersonCamera = IsUsingFirstPersonCamera();

        // Set speed based on the active camera
        speed = isFirstPersonCamera ? 0.0f : 6.0f;
        // Make sure there are cameras in the array
        if (cameras.Length == 0)
        {
            Debug.LogError("No cameras assigned to the Movement script.");
            return;
        }

        // Get the active camera's forward and right directions
        Transform activeCamera = cameras[activeCameraIndex];
        Vector3 cameraForward = Vector3.Scale(activeCamera.forward, new Vector3(1, 0, 1)).normalized;

        // Calculate the move direction based on input and camera direction
        Vector3 move = inputVector.y * cameraForward + inputVector.x * activeCamera.right;

        // Use the Rigidbody's velocity to move the character
        rb.velocity = move * speed;

        if (move.magnitude > 0.1f)
        {
            // Rotate the character to face the move direction
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Set the speed parameter in the animator to control the walk/run animation
        animator.SetFloat("Speed", move.magnitude);
    }

    // Method to switch between cameras (you can call this method based on your game logic)
    public void SwitchCamera(int newIndex)
    {
        if (newIndex >= 0 && newIndex < cameras.Length)
        {
            // Deactivate all cameras
            foreach (Transform cameraTransform in cameras)
        {
            cameraTransform.gameObject.SetActive(false);
        }

            // Activate the selected camera
            cameras[newIndex].gameObject.SetActive(true);

            // Set the active camera index
            activeCameraIndex = newIndex;
        }
        else
        {
            Debug.LogError("Invalid camera index.");
        }
    }
    public bool IsUsingFirstPersonCamera()
    {
        return cameras[activeCameraIndex].gameObject.name == "FirstPersonCamera";
    }
}
