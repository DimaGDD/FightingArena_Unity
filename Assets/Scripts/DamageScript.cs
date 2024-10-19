using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int maxDmg { get; set; }
    public int minDmg { get; set; }
    int RandomChanceForCrit;
    public int CritChance { get; set; }
    public int CritDmg { get; set; }

    public GameObject Sparks;

    AudioManager audioManager;

    private void Start()
    {
        maxDmg = PlayerPrefs.GetInt("SavedMaxDmg", 20);
        minDmg = PlayerPrefs.GetInt("SavedMinDmg", 15);
        CritChance = PlayerPrefs.GetInt("SavedCritChance", 0);
        CritDmg = PlayerPrefs.GetInt("SavedCritDmg", 2);
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            Instantiate(Sparks, transform.position, Quaternion.identity);
            RandomChanceForCrit = Random.Range(1, 101);
            if (RandomChanceForCrit <= CritChance)
            {
                other.GetComponent<EnemyScript>().TakeDamage(Random.Range(minDmg * CritDmg, maxDmg * CritDmg));
            }
            else
            {
                other.GetComponent<EnemyScript>().TakeDamage(Random.Range(minDmg, maxDmg));
            }
            audioManager.PlayAttackingSound();
            audioManager.PlayTakingDamageSound();

        }
    }

   
}
