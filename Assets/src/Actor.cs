using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pixelnest.BulletML;
namespace Jokie
{
    public class Actor : MonoBehaviour, ITriggerable
    {
        private static List<Actor> _actorList = new List<Actor>();
        private static Player _playerShortcut;
        public static Player Player
        {
            get
            {
                return _playerShortcut;
            }
            set
            {
                _playerShortcut = value;
                BulletManagerScript bms = GameObject.FindObjectOfType<BulletManagerScript>();
                if (bms != null)
                {
                    bms.player = value.gameObject;
                }
            }
        }

        public float health;
        private float _armour; // <- There it is. It is never used, but it's there, 5 points! :D
        public float HP
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
                if (health <= 0)
                {
                    Death();
                }
            }
        }


        public static Player GetPlayer()
        {
            return _playerShortcut;
        }


        void Start()
        {
            _actorList.Add(this);
        }

        void OnDestroy()
        {
            _actorList.Remove(this);
        }

        void Update()
        {

        }

        public virtual void Death()
        {
            Destroy(gameObject);
        }

        public void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}