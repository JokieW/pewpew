using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;

namespace Jokie
{
    public class Event : MonoBehaviour, IEvent
    {
        public string CodeWindow = "C# code here";

        public void StartEvent()
        {
            EventManager.RegisterEvent(CodeWindow);
        }
    }
}
