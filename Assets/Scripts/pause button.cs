using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pausebutton : MonoBehaviour
{
    [SerializeField] private GameObject[] _ui;
    [SerializeField] private TextMeshProUGUI[] _textMeshProUGUI;
    public GameObject panel;
    public bool isPause;

    public void Pause()
    { 
        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(false);
        }

        for (int i = 0; i < _textMeshProUGUI.Length; i++)
        {
            _textMeshProUGUI[i].gameObject.SetActive(false);
        }

        isPause = true;

        panel.SetActive(true);
        Time.timeScale = 0f;
    }






}
