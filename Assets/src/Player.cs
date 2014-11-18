using UnityEngine;
using System.Collections;
using Pixelnest.BulletML;

namespace Jokie
{
    public class Player : Actor, ITriggerable
    {
        private Vector3 _movement = Vector3.zero;
        public float Speed = 5.0f;
        public float SpeedModifier = 1.0f;
        public BulletSourceScript[] cannons;
        public MeshRenderer FocusGizmo;

        private Timer TimeSinceDeath = new Timer(0.0f);
        private bool invulnerable = false;

        void Start()
        {
            InputContext ic = gameObject.AddComponent<InputContext>();
            ic.ContextType = InputType.Movement;
            ic.RegisterKeySet(new KeySet(InputAction.Forward, KeyCode.UpArrow, PressType.Held, MoveForward));
            ic.RegisterKeySet(new KeySet(InputAction.Left, KeyCode.LeftArrow, PressType.Held, MoveLeft));
            ic.RegisterKeySet(new KeySet(InputAction.Backward, KeyCode.DownArrow, PressType.Held, MoveBackward));
            ic.RegisterKeySet(new KeySet(InputAction.Right, KeyCode.RightArrow, PressType.Held, MoveRight));

            ic.RegisterKeySet(new KeySet(InputAction.Focus, KeyCode.LeftShift, PressType.Down, Focus));
            ic.RegisterKeySet(new KeySet(InputAction.Focus, KeyCode.LeftShift, PressType.Up, Unfocus));

            ic.RegisterKeySet(new KeySet(InputAction.Bomb, KeyCode.X, PressType.Down, delegate() { Bomb(true);}));

            ic.SetFocus(true);
            InputManager.Register(ic);

            ic = gameObject.AddComponent<InputContext>();
            ic.ContextType = InputType.Combat;
            ic.RegisterKeySet(new KeySet(InputAction.Shoot, KeyCode.Z, PressType.Down, Shoot));
            ic.RegisterKeySet(new KeySet(InputAction.Shoot, KeyCode.Z, PressType.Up, StopShoot));
            ic.SetFocus(true);
            InputManager.Register(ic);

            Player = this;

        }

        public override float HP
        {
            get
            {
                return health;
            }
            set
            {
                if (!invulnerable)
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
        }

        public override void Death()
        {
            Bomb(false);
            SoundEngine.PlayClip("playerDeath");
            health = 1;
            TimeSinceDeath = new Timer(4.0f);
            invulnerable = true;
            transform.position = new Vector3(0.0f, -5.0f, 0.0f);
        }

        void Bomb(bool withSound)
        {
            GameObject.FindObjectOfType<LightController>().Flash();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 7.5f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].GetComponent<BulletScript>() != null)
                {
                    Destroy(hitColliders[i].gameObject);
                }
                i++;
            }
            if (withSound)
            {
                SoundEngine.PlayClip("explosion");
            }
        }

        void Update()
        {
            //Death flicker
            if (invulnerable)
            {
                if (TimeSinceDeath.Check())
                {
                    invulnerable = false;
                    foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                    {
                        mr.enabled = true;
                    }
                }
                else
                {
                    if (TimeSinceDeath.ElapsedSeconds() % 0.5f <= 0.25)
                    {
                        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                        {
                            mr.enabled = false;
                        }
                    }
                    else
                    {
                        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                        {
                            mr.enabled = true;
                        }
                    }
                }
            }

            //Movement
            if (_movement != Vector3.zero)
            {
                //Movement
                Vector3 movement = _movement * Speed * Time.deltaTime;


                //Out of bounds correction
                RaycastHit rayhit;
                Vector3 newPosition = Vector3.zero;

                if (Physics.Raycast(transform.position, new Vector3(_movement.x, 0.0f, 0.0f), out rayhit, Mathf.Abs(movement.x), 1 << 8))
                {
                    newPosition.x = rayhit.point.x + (_movement.x * -0.1f);
                }
                else
                {
                    newPosition.x = transform.position.x + movement.x;
                }

                if (Physics.Raycast(transform.position, new Vector3(0.0f, _movement.y, 0.0f), out rayhit, Mathf.Abs(movement.y), 1 << 8))
                {
                    newPosition.y = rayhit.point.y + (_movement.y * -0.1f);
                }
                else
                {
                    newPosition.y = transform.position.y + movement.y;
                }

                transform.position = newPosition;

                _movement = Vector3.zero;
            }
        }

        void MoveForward()
        {
            _movement += Vector3.up * SpeedModifier;
        }

        void MoveBackward()
        {
            _movement += Vector3.down * SpeedModifier;
        }

        void MoveLeft()
        {
            _movement += Vector3.left * SpeedModifier;
        }

        void MoveRight()
        {
            _movement += Vector3.right * SpeedModifier;
        }

        void Focus()
        {
            SpeedModifier = 0.5f;
            FocusGizmo.enabled = true;
        }

        void Unfocus()
        {
            SpeedModifier = 1.0f;
            FocusGizmo.enabled = false;
        }

        void Shoot()
        {
            foreach (BulletSourceScript bss in cannons)
            {
                bss.Fire();
                bss.Loop = true;
            }
        }

        void StopShoot()
        {
            foreach (BulletSourceScript bss in cannons)
            {
                bss.Loop = false;
            }
        }
    }
}