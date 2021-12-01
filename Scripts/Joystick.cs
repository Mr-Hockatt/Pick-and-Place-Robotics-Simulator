    
using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            print("up arrow key is held down");
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("D arrow key is held down");
        }
    }
}