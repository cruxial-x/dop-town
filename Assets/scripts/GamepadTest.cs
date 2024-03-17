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

        for (int i = 0; i <= 10; i++)
        {
            float axisValue = Input.GetAxis("Axis " + i);
            if (axisValue != 0)
            {
                Debug.Log("Axis " + i + " value: " + axisValue);
            }
        }
    }
}
