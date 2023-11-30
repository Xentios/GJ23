using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UpdateScoreText : MonoBehaviour
{
    [SerializeField]
    private FloatVariable highScore;

    private TextMeshProUGUI textField;
    private void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        textField.text = "Current High Score is: " + highScore.Value;
    }
}
