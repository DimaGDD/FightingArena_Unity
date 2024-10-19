using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider scale;
    public GameObject _continue;

    public GameObject settingsPanel;

    private void Awake()
    {
        settingsPanel.SetActive(true);

        if (!PlayerPrefs.HasKey("NewGame"))
        {
            _continue.SetActive(false);
        }
        else
        {
            _continue.SetActive(true);
        }
    }

    private void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void StartGame(string mode)
    {
        if (mode == "newGame")
        {
            PlayerPrefs.SetInt("NewGame", 1);
            PlayerPrefs.SetFloat("SavedHealth", 100);
            PlayerPrefs.SetInt("SavedMoney", 0);
            PlayerPrefs.SetInt("SavedStage", 1);
            PlayerPrefs.SetInt("SavedMaxDmg", 20);
            PlayerPrefs.SetInt("SavedMinDmg", 15);
            PlayerPrefs.SetInt("SavedCritChance", 0);
            PlayerPrefs.SetInt("SavedCritDmg", 2);
        }

        //SceneManager.LoadScene("TestScene");
        //Time.timeScale = 1.0f;

        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync("TestScene");

        while (!loadAsync.isDone)
        {
            scale.value = loadAsync.progress;

            if (loadAsync.progress >= .9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(2.2f);
                loadAsync.allowSceneActivation = true;
            }

            yield return null;
        }
    }

   public void ExitGame()
   {
        Application.Quit();
   }

   public void SettingsPanel()
   {
        settingsPanel.SetActive(true);
   }

   public void ExitSettings()
   {
        settingsPanel.SetActive(false);
   }
}
