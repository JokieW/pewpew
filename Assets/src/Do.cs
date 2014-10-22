using UnityEngine;
using System.Collections;

namespace EventAction
{
    public static class Do
    {
        public static void Write(string text)
        {
            Debug.Log(text);
        }

        public static void SpawnPlayer(Vector3 position)
        {
            GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Player"), position, Quaternion.identity);
            
        }

        public static void SpawnGrunts(float delay)
        {
            CallbackTimer.RegisterTimer(new Timer(0.0f), SpawnGrunt);
            CallbackTimer.RegisterTimer(new Timer(1.0f), SpawnGrunt);
            CallbackTimer.RegisterTimer(new Timer(2.0f), SpawnGrunt);
        }

        public static void SpawnGrunt()
        {
            GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Grunt"));
        }
    }
}
