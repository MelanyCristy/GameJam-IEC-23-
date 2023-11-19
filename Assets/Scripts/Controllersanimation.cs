using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Controllersanimation : MonoBehaviour
{
   [SerializeField] private InputActionReference gripReference;
    [SerializeField] private InputActionReference triggerReference;
    [SerializeField] private InputActionReference buttonOneReference;
    [SerializeField] private InputActionReference buttonTwoReference;
    [SerializeField] private InputActionReference buttonThreeReference;
    [SerializeField] private InputActionReference joyReference;
    
    [SerializeField] private Animator handAnimator;
   
   
    // Update is called once per frame
    void Update()
    {
        float gripValue = gripReference.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
        
        float triggerValue = triggerReference.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
        
        float buttonOneValue = buttonOneReference.action.ReadValue<float>();
        handAnimator.SetFloat("Button 1", buttonOneValue);
        
        float buttonTwoValue = buttonTwoReference.action.ReadValue<float>();
        handAnimator.SetFloat("Button 2", buttonTwoValue);
        
        float buttonThreeValue = buttonThreeReference.action.ReadValue<float>();
        handAnimator.SetFloat("Button 3", buttonThreeValue);
        
        Vector2 joyValue = joyReference.action.ReadValue<Vector2>();
        handAnimator.SetFloat("Joy X", joyValue.x);
        handAnimator.SetFloat("Joy Y", joyValue.y);
        
        

    }
}
