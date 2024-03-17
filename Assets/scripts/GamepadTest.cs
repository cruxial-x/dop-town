using UnityEngine;
public class GamepadTest : MonoBehaviour
{
    void Update()
    {
        var joystickNames = Input.GetJoystickNames();
        for (int i = 0; i < joystickNames.Length; i++)
        {
            Debug.Log("Joystick " + (i + 1) + ": " + joystickNames[i]);
        }

    for (int i = 0; i < 19; i++)
    {
        if (Input.GetKeyDown("joystick button " + i))
        {
            Debug.Log("Joystick button " + i + " pressed");
        }
    }

    string[] axisNames = { "Horizontal", "Vertical" };

    for (int i = 0; i < axisNames.Length; i++)
    {
        float axisValue = Input.GetAxis(axisNames[i]);
        if (axisValue != 0)
        {
            Debug.Log(axisNames[i] + " value: " + axisValue);
        }
    }
    }
}
