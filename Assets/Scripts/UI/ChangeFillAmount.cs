using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFillAmount : MonoBehaviour
{
    [SerializeField]
    Image imageWithFill;

    [SerializeField]
    FloatVariable amount;

    public void ChangeFillAmountOfImage()
    {
        imageWithFill.fillAmount = amount.Value;

    }
    public void ChangeFillAmountOfImage(float amount)
    {
        imageWithFill.fillAmount = amount;

    }
}
