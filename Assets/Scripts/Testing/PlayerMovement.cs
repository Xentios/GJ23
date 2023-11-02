using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction Move;

    void Start()
    {
        Move.performed += MovementTest;
    }

    private void OnEnable()
    {
        Move.Enable();
    }
    void OnDisable()
    {
        Move.Disable();
    }

    public void MovementTest(InputAction.CallbackContext callback)
    {
        var result=callback.ReadValue<Vector2>();
        var pos = transform.position;

        transform.position = new Vector3(pos.x + result.x, pos.y, pos.z + result.y);
    }

    public void Jump(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Started) return;
        
        transform.position -= Vector3.one;
    }
}
