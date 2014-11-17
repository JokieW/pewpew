using UnityEngine;
using System.Collections;

namespace Jokie
{
    public class BackCameraController : MonoBehaviour, IEvent
    {
        public void StartEvent()
        {
            transform.localPosition = new Vector3(0.0f, 20.0f, 96.0f);
        }

        void Update()
        {
            transform.Translate(Vector3.forward * 15.0f * Time.deltaTime, Space.World);
        }

    }
}