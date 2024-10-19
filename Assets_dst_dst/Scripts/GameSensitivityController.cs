using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameSensitivityController : MonoBehaviour
{
    public Slider sensitivityXSlider;
    public Slider sensitivityYSlider;
    public new CinemachineFreeLook camera;
    public float sensx;
    public float sensy;


    public void Awake()
    {
        if (PlayerPrefs.HasKey("SensX"))
        {
            LoadValue();
        }
        else
        {
            OnSensitivityXChanged();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadValue();
        }
        else
        {
            OnSensitivityYChanged();
        }

        sensx = camera.GetComponent<CameraController>().sensx;
        sensy = camera.GetComponent<CameraController>().sensy;
        sensitivityXSlider.value = sensx;
        sensitivityYSlider.value = sensy;
    }

    public void OnSensitivityXChanged()
    {
        sensx = sensitivityXSlider.value;
        camera.GetComponent<CameraController>().ChangeSensivity("x", sensx);
        PlayerPrefs.SetFloat("SensX", sensx);
    }

    public void OnSensitivityYChanged()
    {
        sensy = sensitivityYSlider.value;
        camera.GetComponent<CameraController>().ChangeSensivity("y", sensy);
        PlayerPrefs.SetFloat("SensY", sensy);
    }

    public void LoadValue()
    {
        sensitivityXSlider.value = PlayerPrefs.GetFloat("SensX");
        OnSensitivityXChanged();

        sensitivityYSlider.value = PlayerPrefs.GetFloat("SensY");
        OnSensitivityYChanged();
    }






















}
