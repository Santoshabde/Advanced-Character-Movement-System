using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardAndMouseInputSystem : IInputSystem
{
    public float HorizontalX()
    {
        return Input.GetAxis("Horizontal");
    }

    public float MouseX()
    {
        return Input.GetAxis("Mouse X");
    }

    public float MouseY()
    {
        return Input.GetAxis("Mouse Y");
    }

    public float VerticalX()
    {
        return Input.GetAxis("Vertical");
    }
}
