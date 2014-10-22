using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour 
{

    private static List<InputContext> contexts = new List<InputContext>();

    public static void Register(InputContext ic)
    {
        contexts.Add(ic);
    }

    public static void Unregister(InputContext ic)
    {
        contexts.Remove(ic);
    }

    public static void GiveFocusTo(InputType type)
    {
        foreach (InputContext ic in contexts)
        {
            if (ic.ContextType == type)
            {
                ic.SetFocus(true);
            }
        }
    }

    public static void RevokeFocusTo(InputType type)
    {
        foreach (InputContext ic in contexts)
        {
            if (ic.ContextType == type)
            {
                ic.SetFocus(false);
            }
        }
    }

    void Start()
    {

    }
}

public enum PressType
{
    Down,
    Held,
    Up
}

public enum InputAction
{
    Forward,
    Backward,
    Left,
    Right,
    Shoot,
    Menu
}

public enum InputType
{
    Movement,
    Combat,
    Menu,
    Text,
    UI,
    All
}