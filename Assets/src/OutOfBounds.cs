using UnityEngine;
using System.Collections;

namespace Jokie
{
    public class OutOfBounds : MonoBehaviour
    {
        void OnCollisionStay(Collision collision)
        {
            Destroy(collision.gameObject);
        }
    }
}