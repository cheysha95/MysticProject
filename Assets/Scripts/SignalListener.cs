using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;
    
    
    public void OnSignalRaised()
    {
        //starts event
        signalEvent.Invoke();
    }

    //onenable regester selves so that the signal knows about us, (add ourselves to list)
    private void OnEnable() {
        signal.RegisterListener(this);
    }
    private void OnDisable() {
        signal.DeRegisterListener(this);
    }
}
