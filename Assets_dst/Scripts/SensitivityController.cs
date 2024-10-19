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
        //sensx = mainMenu.GetComponent<MenuManager>().sensX;
        //sensy = mainMenu.GetComponent<MenuManager>().sensY;
        sensitivityXSlider.value = PlayerPrefs.GetFloat("SensX", 0.2f);
        sensitivityYSlider.value = PlayerPrefs.GetFloat("SensY", 0.002f);
    }

    public void OnSensitivityXChanged()
    {
        sensx = sensitivityXSlider.value;
        PlayerPrefs.SetFloat("SensX", sensx);
        PlayerPrefs.Save();
    }

    public void OnSensitivityYChanged()
    {
        sensy = sensitivityYSlider.value;
        PlayerPrefs.SetFloat("SensY", sensy);
        PlayerPrefs.Save();
    }





















}
