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
    [SerializeField]
    private InputActionReference pauseEditor;

    private void Start()
    {


        restartLevelAction.action.performed += RestartLevel;
        pauseEditor.action.performed += PauseEditorFunction;


    }

    private void OnEnable()
    {
        InputControl.Enable();
    }

    private void OnDisable()
    {

        restartLevelAction.action.performed-=RestartLevel;
        pauseEditor.action.performed -= PauseEditorFunction;

        InputControl.Disable();

    }


    private void RestartLevel(InputAction.CallbackContext callbackContext)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PauseEditorFunction(InputAction.CallbackContext callbackContext)
    {
        Debug.Break();
    }

}
