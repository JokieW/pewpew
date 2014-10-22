using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour 
{
    void OnTriggerExit(Collider collider)
    {
        collider.SendMessage("OnOutOfBounds", SendMessageOptions.DontRequireReceiver);
    }
}
