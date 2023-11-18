using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderValueChanger : MonoBehaviour
{

    [SerializeField]
    public float FinalValue;
    [SerializeField]
    private float timer;
    // Start is called before the first frame update

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        DOVirtual.Float(0, FinalValue, timer, result => slider.value =result);
    }

    
}
