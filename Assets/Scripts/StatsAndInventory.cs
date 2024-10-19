using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StatsAndInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HpText;
    [SerializeField] private TextMeshProUGUI MaxDmgText;
    [SerializeField] private TextMeshProUGUI MinDmgText;
    [SerializeField] private TextMeshProUGUI CritChanceText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ColliderAttack;

    private void Start()
    {
        UpdateHpText();
        UpdateMaxDmgText();
        UpdateMinDmgText();
        UpdateCritChanceText();
    }

    private void Update()
    {
        UpdateHpText();
        UpdateMaxDmgText();
        UpdateMinDmgText();
        UpdateCritChanceText();
    }
    public void UpdateHpText()
    {
        HpText.text = "Health:" + player.GetComponent<PlayerMovementAndroid>().health;
    }

    public void UpdateMaxDmgText()
    {
        MaxDmgText.text = "Max Dmg:" + ColliderAttack.GetComponent<DamageScript>().maxDmg;
    }

    public void UpdateMinDmgText()
    {
        MinDmgText.text = "Min Dmg:" + ColliderAttack.GetComponent<DamageScript>().minDmg;
    }

    public void UpdateCritChanceText()
    {
        CritChanceText.text = "Crit Chance:" + ColliderAttack.GetComponent<DamageScript>().CritChance;
    }





















}
