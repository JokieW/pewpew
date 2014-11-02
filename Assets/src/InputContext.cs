using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Jokie
{
    public class InputContext : MonoBehaviour
    {
        private bool _focused = false;

        private InputType _inputType = InputType.All;
        public InputType ContextType
        {
            get
            {
                return _inputType;
            }
            set
            {
                _inputType = value;
            }
        }

        private List<KeySet> _keySets = new List<KeySet>();

        public void RegisterKeySet(KeySet set)
        {
            _keySets.Add(set);
        }

        void Start()
        {

        }


        void Update()
        {
            if (_focused)
            {
                foreach (KeySet ks in _keySets)
                {
                    if (Input.anyKey)
                    {
                        if (ks.Press == PressType.Down)
                        {
                            if (Input.GetKeyDown(ks.Key))
                            {
                                ks.Function.Invoke();
                            }
                        }
                        else if (ks.Press == PressType.Held)
                        {
                            if (Input.GetKey(ks.Key))
                            {
                                ks.Function.Invoke();
                            }
                        }
                    }

                    if (ks.Press == PressType.Up)
                    {
                        if (Input.GetKeyUp(ks.Key))
                        {
                            ks.Function.Invoke();
                        }
                    }
                }
            }
        }

        public void SetFocus(bool focus)
        {
            _focused = focus;

            if (!focus)
            {
                foreach (KeySet ks in _keySets)
                {
                    if (ks.Press == PressType.Up)
                    {
                        ks.Function.Invoke();
                    }
                }
            }
        }
    }

    public struct KeySet
    {
        public KeySet(InputAction Action, KeyCode Key, PressType Press, Action Function)
        {
            this.Action = Action;
            this.Key = Key;
            this.Press = Press;
            this.Function = Function;
        }

        public InputAction Action;
        public KeyCode Key;
        public PressType Press;
        public Action Function;
    }
}