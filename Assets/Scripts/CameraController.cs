using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public TouchCamera touchCamera;

    public Cinemachine.CinemachineFreeLook freeLookCamera;

    public float sensivityHorizontalRotate = 1f;
    public float sensivityVerticalRotate = 0.02f;
    public float sensx;
    public float sensy;
    private void Awake()
    {
        sensx = PlayerPrefs.GetFloat("sensx", 0.2f);
        sensy = PlayerPrefs.GetFloat("sensy", 0.002f);

    }
    private void Update()
    {
        float mouseX = 0;
        float mouseY = 0;

        if (touchCamera.pressed)
        {
            Touch touch = Input.GetTouch(touchCamera.fingerId);
            if (touch.phase == TouchPhase.Moved && touch.position.x > Screen.width / 2)
            {
                mouseY = touch.deltaPosition.y * sensy;
                mouseX = touch.deltaPosition.x * sensx;
            }
        }

        freeLookCamera.m_XAxis.Value += mouseX;
        freeLookCamera.m_YAxis.Value += mouseY;
    }

    public void ChangeSensivity(string axise, float value)
    {
        if (axise == "x")
        {
            sensx = value;
        }
        else if (axise == "y")
        {
            sensy = value;
        }
    }
}
