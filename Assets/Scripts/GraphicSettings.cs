using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private int quality { get; set; }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("SavedQualityIndex", qualityIndex);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        quality = PlayerPrefs.GetInt("SavedQualityIndex", 2);
        QualitySettings.SetQualityLevel(quality);
        dropdown.value = quality;
    }
}

