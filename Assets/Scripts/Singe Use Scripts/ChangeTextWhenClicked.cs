using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class ChangeTextWhenClicked : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private InputActionReference mouseClick;

    private TextMeshProUGUI textField;

    private void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        mouseClick.action.performed += MouseClicked;
        
    }

    private void OnDisable()
    {
        mouseClick.action.performed -= MouseClicked;
    }

    private void MouseClicked(InputAction.CallbackContext callbackContext)
    {
        if (button.IsInteractable() == false) return;

        textField.text = "NEXT";
    }

    public void ResetText()
    {
        textField.text = "SKIP";
    }
}
