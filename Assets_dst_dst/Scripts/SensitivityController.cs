using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SensitivityController : MonoBehaviour
{
    public Slider sensitivityXSlider;
    public Slider sensitivityYSlider;
    public Canvas mainMenu;
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

        sensx = mainMenu.GetComponent<MenuManager>().sensX;
        sensy = mainMenu.GetComponent<MenuManager>().sensY;
        sensitivityXSlider.value = sensx;
        sensitivityYSlider.value = sensy;
    }

    public void OnSensitivityXChanged()
    {
        sensx = sensitivityXSlider.value;
        mainMenu.GetComponent<MenuManager>().ChangeSensivity("x", sensx);
        PlayerPrefs.SetFloat("SensX", sensx);
    }

    public void OnSensitivityYChanged()
    {
        sensy = sensitivityYSlider.value;
        mainMenu.GetComponent<MenuManager>().ChangeSensivity("y", sensy);
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
