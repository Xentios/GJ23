using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset InputControl;

    [SerializeField]
    private InputActionReference restartLevelAction;

    private void Start()
    {


        restartLevelAction.action.performed += RestartLevel;
        
    }

    private void OnEnable()
    {
        InputControl.Enable();
    }

    private void OnDisable()
    {

        restartLevelAction.action.performed-=RestartLevel;
     
        InputControl.Disable();

    }


    private void RestartLevel(InputAction.CallbackContext callbackContext)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
