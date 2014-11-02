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
        public float HP
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
                }

                if (tag == "Player")
                {
                    GameObject.Find("HP").GetComponent<TextMesh>().text = "HP: " + health;
                }
            }
        }

        public float armour; // <- There it is. It is never used, but it's there, 5 points! :D
        private float MitigatedDamage(int damage) // <- There it is, can I have 2 points retroactively?
        {
            return Mathf.Clamp(damage - armour, 0.0f, Mathf.Infinity);
        }

        public int score;

        void Start()
        {
            _actorList.Add(this);
        }

        void OnDestroy()
        {
            _actorList.Remove(this);
            if (tag == "BadGuy")
            {
                EnemyManager.TANGODOWN();
            }
        }

        void Update()
        {

        }

        public virtual void Death()
        {
            if (tag == "BadGuy")
            {
                Player.Player.Score += score;
            }
            Destroy(gameObject);
        }
    }
}