using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausebutton : MonoBehaviour
{
    [SerializeField] private GameObject[] _ui;
    public GameObject panel;
    public bool isPause;

    public void Pause()
    { 
        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(false);
        }

        isPause = true;

        panel.SetActive(true);
        Time.timeScale = 0f;
    }






}
