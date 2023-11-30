using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTextWithEvents : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textFieldToChange;

    [SerializeField]
    private string newText;

    private string oldText;
    public void ChangeToNewText()
    {
        oldText = textFieldToChange.text;
        textFieldToChange.text = newText;       
    }

    public void Revert()
    {         
        textFieldToChange.text = oldText;
    }

}
