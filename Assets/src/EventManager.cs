using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;

public class EventManager : MonoBehaviour 
{
    private static List<string> _events = new List<string>();

    public static void RegisterEvent(string eventText)
    {
        _events.Add(eventText);
    }

	void Update () 
    {
	    if(_events.Count > 0)
        {
            Mono.CSharp.Evaluator.Init(new string[] { });
            foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.Contains("Assembly-CSharp"))
                {
                    Mono.CSharp.Evaluator.ReferenceAssembly(assembly);
                }
                if (assembly.FullName.Contains("UnityEngine"))
                {
                    Mono.CSharp.Evaluator.ReferenceAssembly(assembly);
                }
                if (assembly.FullName.Contains("Cecil"))
                {
                    Mono.CSharp.Evaluator.ReferenceAssembly(assembly);
                }
            }

            foreach(string s in _events)
            {
                Mono.CSharp.Evaluator.Run("using EventAction; using UnityEngine;");
                Evaluator.Run(s);
            }
            _events.Clear();
        }
	}
}
