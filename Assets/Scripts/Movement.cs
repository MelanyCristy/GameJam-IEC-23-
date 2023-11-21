using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Make the Rigidbody public so you can assign it in the inspector
    public Rigidbody rb;
    public Animator animator;
    public InputActionReference moveAction;
    public float speed = 6.0f;
    public float rotationSpeed = 700.0f;
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
        Vector3 move = new Vector3(inputVector.x, 0, inputVector.y);
        // Use the Rigidbody's velocity to move the character
        rb.velocity = move * speed;

        if (move != Vector3.zero)
        {
           gameObject.transform.forward = move;
        
        }

        // Set the speed parameter in the animator to control the walk/run animation
        animator.SetFloat("Speed", move.magnitude);
    }
}
