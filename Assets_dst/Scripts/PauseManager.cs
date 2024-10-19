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
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _triggerArena;
    [SerializeField] private GameObject _damageScript;

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
        PlayerPrefs.SetFloat("SavedHealth", _player.GetComponent<PlayerMovementAndroid>().health);
        PlayerPrefs.SetInt("SavedMoney", _player.GetComponent<PlayerMovementAndroid>().money);
        PlayerPrefs.SetInt("SavedStage", _triggerArena.GetComponent<TriggerForArena>().numberOfStage);
        PlayerPrefs.SetInt("SavedMaxDmg", _damageScript.GetComponent<DamageScript>().maxDmg);
        PlayerPrefs.SetInt("SavedMinDmg", _damageScript.GetComponent<DamageScript>().minDmg);
        PlayerPrefs.SetInt("SavedCritChance", _damageScript.GetComponent<DamageScript>().CritChance);
        PlayerPrefs.SetInt("SavedCritDmg", _damageScript.GetComponent<DamageScript>().CritDmg);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");
    }
}
