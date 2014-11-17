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
        public virtual float HP
        {
            get
            {
                return health;
            }
            set
            {
                if (value <= 0)
                {
                    if (health > 0)
                    {
                        health = 0;
                        Death();
                    }
                }
                else
                {
                    health = value;
                    if (health % 10 == 0)
                    {
                        SoundEngine.PlayClip("damage");
                    }
                }
            }
        }

        void Start()
        {
            _actorList.Add(this);
        }

        void OnDestroy()
        {
            _actorList.Remove(this);
        }

        public virtual void Death()
        {
            SoundEngine.PlayClip("enemyDeath");
            Destroy(gameObject);
        }
    }
}