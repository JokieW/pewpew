using UnityEngine;
using System.Collections;

namespace Jokie.EventAction
{
    public static class Do
    {
        public static void Write(string text)
        {
            Debug.Log(text);
        }

        public static void SpawnPlayer(Vector3 position)
        {
            EnemyManager.GOBACKINTIMETOKICKTHOSESUCKERSAGAIN();
            GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Player"), position, Quaternion.identity);
        }
    }
}
