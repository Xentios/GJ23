using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Slider))]
public class SliderValueChanger : MonoBehaviour
{

    [SerializeField]
    public float FinalValue;
    [SerializeField]
    private float timer;
    // Start is called before the first frame update

    private Slider slider;

    [SerializeField]
    private UnityEvent nextPanel;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void OnEnable()
    {
        DOVirtual.Float(0, FinalValue, timer, result => slider.value = result).OnComplete(() =>
         nextPanel.Invoke()
         );
    }

    
}
