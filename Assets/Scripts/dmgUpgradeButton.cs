using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class dmgUpgradeButton : MonoBehaviour
{
    public float radius = 50f;
    [SerializeField] private Transform _center;
    [SerializeField] private GameObject _use;
    [SerializeField] private GameObject colliderAttack;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI upgradePriceText;
    [SerializeField] private TextMeshProUGUI upgradesCounter;

    public GameObject Effect;
    public int upgradesBought { get; set; }
    private int baseUpgradePrice = 100;
    private int priceIncreasePerUpgrade = 50;

    AudioManager audioManager;

    public bool isPlayerInCollider = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        upgradesBought = PlayerPrefs.GetInt("SavedDmgCost", 0);
        UpdateUpgradePriceText();
        UpdateCounterText();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInCollider = true;

            _use.SetActive(true);
            upgradesCounter.gameObject.SetActive(true);
            upgradePriceText.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInCollider = false;
            _use.SetActive(false);
            upgradesCounter.gameObject.SetActive(false);
            upgradePriceText.gameObject.SetActive(false);
        }
    }

    public void UpdateDamage()
    {
        if ((player.GetComponent<PlayerMovementAndroid>().money >= GetCurrentUpgradePrice()) && (upgradesBought < 10))
        {
            player.GetComponent<PlayerMovementAndroid>().money -= GetCurrentUpgradePrice();
            audioManager.PlayTrueBuy();

            colliderAttack.GetComponent<DamageScript>().minDmg += 5;
            colliderAttack.GetComponent<DamageScript>().maxDmg += 5;

            Quaternion spawnRotation = Quaternion.Euler(-28.504f, -206.062f, -270.31f);
            Instantiate(Effect, player.transform.position, spawnRotation);
            
            upgradesBought++;
            UpdateUpgradePriceText();
            UpdateCounterText();
        }
        else
        {
            audioManager.PlayFalseBuy();
        }
    }

    public void ResetDmg()
    {
        colliderAttack.GetComponent<DamageScript>().minDmg = 15;
        colliderAttack.GetComponent<DamageScript>().maxDmg = 20;

        upgradesBought = 0;

        UpdateUpgradePriceText();
        UpdateCounterText();
    }

    private int GetCurrentUpgradePrice()
    {
        return baseUpgradePrice + (upgradesBought * priceIncreasePerUpgrade);
    }

    private void UpdateUpgradePriceText()
    {
        if (upgradePriceText != null)
        {
            upgradePriceText.text = "Cost: " + GetCurrentUpgradePrice().ToString();
        }
    }

    private void UpdateCounterText()
    {
        if (upgradesCounter != null)
        {
            upgradesCounter.text = upgradesBought.ToString() + "/10";
        }
    }

}
