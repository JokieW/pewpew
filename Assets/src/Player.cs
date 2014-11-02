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

        private int _score = 0;
        public int Score
        {
            get
            {
                return _score;   
            }
            set
            {
                _score = value;
                GameObject.Find("Score").GetComponent<TextMesh>().text = "Score: " + value;
            }
        }

        private int _lives = 3;
        public int Lives
        {
            get
            {
                return _lives;
            }
            set
            {
                if (_lives > 0)
                {
                    _lives = value;
                    GameObject.Find("Lives").GetComponent<TextMesh>().text = "Lives: " + value;
                    if (_lives > 0)
                    {
                        HP = 1;
                    }
                    else
                    {
                        GameObject.Find("Gameover").GetComponent<TextMesh>().text = "U R DED";
                        CallbackTimer.RegisterTimer(new Timer(5.0f), delegate { Application.LoadLevel("Menu"); });
                        InputManager.RevokeFocusTo(InputType.Movement);
                        InputManager.RevokeFocusTo(InputType.Combat);
                    }
                }
            }
        }

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

        public override void Death()
        {
            Lives--;
        }

        void Update()
        {
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
        }

        void Unfocus()
        {
            SpeedModifier = 1.0f;
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