using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    private UnityEvent<float> changeSlider;

 
    [SerializeField]
    private GameObject spindle;

    [SerializeField]
    private float animationSpeed = 1f;



    private Animator animator;
    private float animationState;

    [SerializeField]
    private float setPressTimer;

  
    private float pressTimer;
    private void OnEnable()
    {
        pressTimer = setPressTimer;
        var targetObject=GameManager.Instance.targetObject;
        targetObject.transform.parent = new GameObject("targetPivot").transform;
        var tPos=targetObject.transform.position;
        tPos.y = 2;
        targetObject.transform.position = tPos;
        targetObject.transform.parent.position = new Vector3(0, -4, 0);

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
        mouseMovement.action.performed -= MouseMovementEvent;
        mouseLeftDown.action.started -= MouseLeftDownEvent;
        mouseLeftUp.action.canceled -= MouseLeftUpEvent;


        //mouseMovement.action.Disable();
        mouseLeftDown.action.Disable();
        mouseLeftUp.action.Disable();
    }

    private void Update()
    {
        if (pressTimer < 0) return;

        animator.SetFloat("Speed", animationState);
        animationState -= 0.1f * Time.deltaTime;
        animationState = Mathf.Max(0f, animationState);
        pressTimer -= Time.deltaTime;
        ScaleDown(animationState);
        if (pressTimer < 0)
        {
            jobFinished.TriggerEvent();
        }
    }


    private void MouseMovementEvent(InputAction.CallbackContext callbackContext)
    {
        var delta = callbackContext.ReadValue<Vector2>();
        Debugger.Log("Mouse Delta is " + delta.ToString(), Debugger.PriorityLevel.LeastImportant);


        float rotX = delta.x;
        float rotY = delta.y;

        var displacement = rotY;
        //This is normal spin
        //Vector3 right = Vector3.Cross(Camera.main.transform.up, spindle.transform.position - Camera.main.transform.position);
        //Vector3 up = Vector3.Cross(spindle.transform.position - Camera.main.transform.position, right);
        ////spindle.transform.rotation *= Quaternion.AngleAxis(-rotX, up);
        ////spindle.transform.rotation *= Quaternion.AngleAxis(rotY, right);
        if (displacement < 0)
        {
            spindle.transform.localRotation *= Quaternion.AngleAxis(displacement, transform.right);
            var anispeed = -1 * ((displacement) / animationSpeed);
            animationState += anispeed;
            // SetAnimatorSpeed(anispeed);
        }
        else
        {
            spindle.transform.localRotation *= Quaternion.AngleAxis((displacement) / 3, transform.right);
            var anispeed = -1 * ((displacement) / animationSpeed);
            animationState += anispeed / 2;
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

    private void ScaleDown(float state)
    {
        //var max = animator.GetCurrentAnimatorStateInfo(0).length;
        var resultY = MapF(state, 0, 1f, 0, 1);
        resultY = Mathf.Max(1.0f - resultY,0.001f);
        var currenY = GameManager.Instance.targetObject.transform.parent.localScale.y;
        if (currenY > resultY)
        {
            var scale = GameManager.Instance.targetObject.transform.parent.localScale;
            scale.y = resultY;
            GameManager.Instance.targetObject.transform.parent.localScale = scale;
            changeSlider.Invoke(resultY);
        }
    }

    private float MapF(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
