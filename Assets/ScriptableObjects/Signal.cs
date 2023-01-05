using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Signal : ScriptableObject
{
    //list of every signal active
    public List<SignalListener> listeners = new List<SignalListener>();

    
    //goes through list backward (prevents out of range execptions if one item removes itself, calling onraised in signal listener, this effects every object with a listener)
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnSignalRaised();
        }
    }
    
    //regestirs signal listener with somthing, registers the player with system
    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }
    
}
