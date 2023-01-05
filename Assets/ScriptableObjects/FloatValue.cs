using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//needed
[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    
    [NonSerialized] // stops from apperaing in inspector
    public float RuntimeValue;



    //---------------------
    public void OnAfterDeserialize()
    { // resets value everytime we restart game
        RuntimeValue = initialValue;
    }
    
    public void OnBeforeSerialize() { }
}