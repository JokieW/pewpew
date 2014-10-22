using UnityEngine;
using System.Collections;

//Trigger warning
public class Trigger : MonoBehaviour 
{
    public TriggerType type;
    public Event triggeree;

	void Start () 
    {
        if (type == TriggerType.Autorun)
        {
            triggeree.StartEvent();
        }
	}

    void FireTrigger()
    {
        if (type == TriggerType.PlayerInteract)
        {
            triggeree.StartEvent();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(type == TriggerType.PlayerTouch || type == TriggerType.EventTouch || type == TriggerType.ActorTouch)
        {
            ITriggerable it = (ITriggerable)other.gameObject.GetComponent(typeof(ITriggerable));
            if (it != null)
            {
                if (type == TriggerType.PlayerTouch && it.GetType() == typeof(Player))
                {
                    triggeree.StartEvent();
                }
                else if (type == TriggerType.ActorTouch && it.GetType() == typeof(Actor))
                {
                    triggeree.StartEvent();
                }
                else if (type == TriggerType.EventTouch && it.GetType() == typeof(Event))
                {
                    triggeree.StartEvent();
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
    Autorun
}
