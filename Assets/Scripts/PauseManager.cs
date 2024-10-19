using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject settingsPanel;
    public GameObject TipsMenu;
    [SerializeField] private GameObject[] _ui;
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _triggerArena;
    [SerializeField] private GameObject _damageScript;
    [SerializeField] private GameObject _dmg;
    [SerializeField] private GameObject _crit;
    [SerializeField] private GameObject _heal;
    [SerializeField] private GameObject _useLongHeal;
    [SerializeField] private GameObject _dmgUpgradeButton;
    [SerializeField] private GameObject _critChanceButtonUpgrade;
    [SerializeField] private TextMeshProUGUI[] _textMeshProUGUI;


    public void Start()
    {
        TipsMenu.SetActive(true);
    }

    public void CloseTips()
    {
        TipsMenu.SetActive(false);
    }

    public void OpenTips()
    {
        TipsMenu.SetActive(true);
    }


    public void Continue()
    {
        for (int i = 0; i < _ui.Length; i++)
        {
            if (i == 6)
            {
                if (_triggerArena.GetComponent<TriggerForArena>().isPlayerInCollider)
                {
                    _ui[i].SetActive(true);
                }
            }
            else if (i == 7)
            {
                if (_dmg.GetComponent<dmgUpgradeButton>().isPlayerInCollider)
                {
                    _ui[i].SetActive(true);
                    _textMeshProUGUI[0].gameObject.SetActive(true);
                    _textMeshProUGUI[1].gameObject.SetActive(true);
                }
            }
            else if (i == 8)
            {
                if (_crit.GetComponent<critChanceButtonClick>().isPlayerInCollider)
                {
                    _ui[i].SetActive(true);
                    _textMeshProUGUI[2].gameObject.SetActive(true);
                    _textMeshProUGUI[3].gameObject.SetActive(true);
                }
            }
            else if (i == 9)
            {
                if (_heal.GetComponent<UseHealButton>().isPlayerInCollider)
                {
                    _ui[i].SetActive(true);
                    _textMeshProUGUI[4].gameObject.SetActive(true);
                }
            }
            else if ( i == 10)
            {
                if (_useLongHeal.GetComponent<UseLongHeal>().step == 0)
                {
                    _ui[i].SetActive(true);
                }
            }
            else if (i == 11)
            {
                if (_useLongHeal.GetComponent<UseLongHeal>().step == 2)
                {
                    _ui[i].SetActive(true);
                }
            }
            else
            {
                _ui[i].SetActive(true);
            }
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
        PlayerPrefs.SetInt("SavedDmgCost", _dmgUpgradeButton.GetComponent<dmgUpgradeButton>().upgradesBought);
        PlayerPrefs.SetInt("SavedCritChanceCost", _critChanceButtonUpgrade.GetComponent<critChanceButtonClick>().upgradesBought);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");
    }
}
