using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;

public class Event : MonoBehaviour, ITriggerable
{
    public string CodeWindow = "C# code here";

	void Start () 
    {

	}
	
	void Update () 
    {
	    
	}

    public void StartEvent()
    {
        EventManager.RegisterEvent(CodeWindow);
    }



}
