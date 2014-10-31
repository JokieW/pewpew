using UnityEngine;
using System;
using System.Collections.Generic;

namespace Jokie
{
    public class CallbackTimer : MonoBehaviour
    {
        static private Dictionary<Timer, Action> _timers = new Dictionary<Timer, Action>();

        public static void RegisterTimer(Timer timer, Action callback)
        {
            _timers.Add(timer, callback);
        }

        void Update()
        {
            if (_timers.Count > 0)
            {
                List<Timer> deadTimers = null;
                foreach (KeyValuePair<Timer, Action> kvp in _timers)
                {
                    if (kvp.Key.Check())
                    {
                        if (deadTimers == null)
                        {
                            deadTimers = new List<Timer>();
                        }
                        kvp.Value.Invoke();
                        deadTimers.Add(kvp.Key);
                    }
                }
                if (deadTimers != null)
                {
                    foreach (Timer t in deadTimers)
                    {
                        _timers.Remove(t);
                    }
                }
            }
        }
    }
}