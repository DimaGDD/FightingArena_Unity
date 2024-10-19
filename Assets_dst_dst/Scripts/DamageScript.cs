using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int maxDmg = 20;
    public int minDmg = 15;
    int RandomChanceForCrit;
    public int CritChance = 0;
    public int CritDmg = 2;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
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
