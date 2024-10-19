using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject settingsPanel;
    [SerializeField] private GameObject[] _ui;
    [SerializeField] private GameObject _pause;

    public void Continue()
    {
        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(true);
        }

        Time.timeScale = 1.0f;
        panel.SetActive(false);

        _pause.GetComponent<pausebutton>().isPause = false;

    }

    public void Settings()
    {
        panel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ExitSettings()
    {
        settingsPanel.SetActive(false);
        panel.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
