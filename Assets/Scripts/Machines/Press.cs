using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Press : MonoBehaviour
{
    [SerializeField]
    private GameEvent jobFinished;

    [SerializeField]
    private InputActionReference mouseMovement;
    [SerializeField]
    private InputActionReference mouseLeftDown;
    [SerializeField]
    private InputActionReference mouseLeftUp;





    [SerializeField]
    private GameObject spindle;

    [SerializeField]
    private float animationSpeed=1f;



    private Animator animator;
    private float animationState;

    [SerializeField]
    private float pressTimer;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        SetAnimatorSpeed();

        mouseMovement.action.performed += MouseMovementEvent;
        mouseLeftDown.action.started += MouseLeftDownEvent;
        mouseLeftUp.action.canceled += MouseLeftUpEvent;


        mouseMovement.action.Enable();
        mouseLeftDown.action.Enable();
        mouseLeftUp.action.Enable();

    }
    void OnDisable()
    {
        mouseMovement.action.performed-=MouseMovementEvent;
        mouseLeftDown.action.started -=MouseLeftDownEvent;
        mouseLeftUp.action.canceled-= MouseLeftUpEvent;


        mouseMovement.action.Disable();
        mouseLeftDown.action.Disable();
        mouseLeftUp.action.Disable();
    }

    private void Update()
    {
        if (pressTimer < 0) return;

        animator.SetFloat("Speed", animationState);
        animationState -= 0.1f*Time.deltaTime;
        pressTimer -= Time.deltaTime;
        if (pressTimer < 0)
        {
            jobFinished.TriggerEvent();
        }
    }


    private void MouseMovementEvent(InputAction.CallbackContext callbackContext)
    {
        var delta = callbackContext.ReadValue<Vector2>();
        Debugger.Log( "Mouse Delta is "+ delta.ToString(),Debugger.PriorityLevel.LeastImportant);


        float rotX = delta.x;
        float rotY = delta.y;

        //This is normal spin
        //Vector3 right = Vector3.Cross(Camera.main.transform.up, spindle.transform.position - Camera.main.transform.position);
        //Vector3 up = Vector3.Cross(spindle.transform.position - Camera.main.transform.position, right);
        ////spindle.transform.rotation *= Quaternion.AngleAxis(-rotX, up);
        ////spindle.transform.rotation *= Quaternion.AngleAxis(rotY, right);
        if(rotX + rotY < 0)
        {
            spindle.transform.localRotation *= Quaternion.AngleAxis(rotX + rotY, transform.right);
            var anispeed = -1*( (rotX + rotY)/animationSpeed);
            animationState += anispeed;
           // SetAnimatorSpeed(anispeed);
        }
        else
        {
            spindle.transform.localRotation *= Quaternion.AngleAxis((rotX + rotY)/3, transform.right);
            var anispeed = -1 * ((rotX + rotY) / animationSpeed);
            animationState += anispeed/2;
        }
    }

    private void MouseLeftDownEvent(InputAction.CallbackContext callbackContext)
    {

    }
    private void MouseLeftUpEvent(InputAction.CallbackContext callbackContext)
    {
        //SetAnimatorSpeed(-0.1f);
    }

    private void SetAnimatorSpeed()
    {
        //animator.speed = 0;
    }

    private void SetAnimatorSpeed(float value)
    {
        animator.SetFloat("Speed", value);
       // animator.speed = value;
    }
  
    //private void OnTriggerStay(Collider other)
    //{
    //    var currentScale = other.transform.localScale;
    //    currentScale.y -= 0.1f;
    //    other.transform.localScale = currentScale;
    //}
}
