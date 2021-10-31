using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInput : MonoBehaviour
{
    public int VerticalInput()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
            return 1;
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            return -1;
        else
            return 0;
    }

    public int HorizontalInput()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            return -1;
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            return 1;
        else
            return 0;
    }

    public bool WaitInput()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }

    public bool RestartInput()
    {
        return Input.GetKeyUp(KeyCode.R);
    }

    public bool UndoInput()
    {
        return Input.GetKeyDown(KeyCode.U);
    }
}
