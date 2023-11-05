using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EasterEggClicked : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("EasterEggClicked");//,Debugger.PriorityLevel.High);
    }

    private void OnMouseOver()
    {
        Debug.Log("asdsadsad");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");//,Debugger.PriorityLevel.High);
    }
}
