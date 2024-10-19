using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider scale;

    public float sensX = 0.2f;
    public float sensY = 0.002f;

    public GameObject settingsPanel;

    public void StartGame()
    {
        PlayerPrefs.SetFloat("sensx", sensX);
        PlayerPrefs.SetFloat("sensy", sensY);
        PlayerPrefs.Save();
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

    public void ChangeSensivity(string axise, float value)
    {
        if (axise == "x")
        {
            sensX = value;
        }
        else if (axise == "y")
        {
            sensY = value;
        }
    }
}
