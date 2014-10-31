using UnityEngine;
using System.Collections;

namespace Jokie
{
    //Trigger warning
    public class Trigger : MonoBehaviour
    {
        public TriggerType type;
        public Object triggeree;
        public float timer;

        void Start()
        {
            if (type == TriggerType.Autorun)
            {
                ((IEvent)triggeree).StartEvent();
            }
            else if (type == TriggerType.Timed)
            {
                CallbackTimer.RegisterTimer(new Timer(timer), ((IEvent)triggeree).StartEvent);
            }
        }

        void FireTrigger()
        {
            if (type == TriggerType.PlayerInteract)
            {
                ((IEvent)triggeree).StartEvent();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (type == TriggerType.PlayerTouch || type == TriggerType.EventTouch || type == TriggerType.ActorTouch)
            {
                ITriggerable it = (ITriggerable)other.gameObject.GetComponent(typeof(ITriggerable));
                if (it != null)
                {
                    if (type == TriggerType.PlayerTouch && it.GetType() == typeof(Player))
                    {
                        ((IEvent)triggeree).StartEvent();
                    }
                    else if (type == TriggerType.ActorTouch && it.GetType() == typeof(Actor))
                    {
                        ((IEvent)triggeree).StartEvent();
                    }
                    else if (type == TriggerType.EventTouch && it.GetType() == typeof(Event))
                    {
                        ((IEvent)triggeree).StartEvent();
                    }
                }
            }
        }
    }

    public enum TriggerType
    {
        None,
        PlayerTouch,
        ActorTouch,
        EventTouch,
        PlayerInteract,
        Autorun,
        Timed
    }
}