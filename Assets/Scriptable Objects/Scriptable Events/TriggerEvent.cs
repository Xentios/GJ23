using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    private  UnityEvent unityEvents;
    [SerializeField]
    private List<GameEvent> gameEvents;


    [SerializeField]
    private float timeToTrigger;

    [SerializeField]
    private bool TriggerOnAwake;
    [SerializeField]
    private bool TriggerOnStart;
    [SerializeField]
    private bool TriggerOnEnable;
    [SerializeField]
    private bool TriggerOnDisable;
    [SerializeField]
    private bool TriggerOnDestroy;
    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    private void Awake()
    {
        if (TriggerOnAwake == true)
        {
            TriggerInvokeAll();
        }
    }
    void Start()
    {
        if (TriggerOnStart == true)
        {
            TriggerInvokeAll();
        }
    }

    private void OnEnable()
    {
        if (TriggerOnEnable == true)
        {
            TriggerInvokeAll();
        }
    }

    private void OnDisable()
    {
        if (TriggerOnDisable == true)
        {
            TriggerInvokeAll();
        }
    }

    private void OnDestroy()
    {
        if (TriggerOnDestroy == true)
        {
            TriggerInvokeAll();
        }
    }


    [ContextMenu("TriggerEvents")]
    public void TriggerInvokeAll()
    {
        if (timeToTrigger <= 0)
        {
            TIALL();
        }
        else
        {
            StartCoroutine(TriggerAllWithTime());
        }
        
    }


    IEnumerator TriggerAllWithTime()
    {
        yield return new WaitForSeconds(timeToTrigger);
        TIALL();
    }


    private void TIALL()
    {
        unityEvents.Invoke();
        foreach (var gameEvent in gameEvents)
        {
            gameEvent.TriggerEvent();
        }
    }
}
