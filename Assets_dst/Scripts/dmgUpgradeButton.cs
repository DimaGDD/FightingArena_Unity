using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class dmgUpgradeButton : MonoBehaviour
{
    public float radius = 50f;
    [SerializeField] private Transform _center;
    [SerializeField] private GameObject _use;
    [SerializeField] private GameObject[] Colliders;
    [SerializeField] private GameObject player;
 
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private bool IsInRadius()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            Vector3 enemyPosition = player.transform.position;
            Vector3 direction = enemyPosition - _center.position;

            if (direction.magnitude <= radius)
                return true;
        }

        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_center.position, radius);
    }

    private void Update()
    {
        if (IsInRadius())
        {
            _use.SetActive(true);
        }
        else
        {
            _use.SetActive(false);
        }
    }

    public void UpdateDamage()
    {
        if (player.GetComponent<PlayerMovementAndroid>().money >= 100)
        {
            player.GetComponent<PlayerMovementAndroid>().money -= 100;
            audioManager.PlayTrueBuy();

            foreach (GameObject collider in Colliders) 
            {
                collider.GetComponent<DamageScript>().minDmg += 5;
                collider.GetComponent<DamageScript>().maxDmg += 5;
            }
        }
        else
        {
            audioManager.PlayFalseBuy();
        }
    }

    public void ResetDmg()
    {
        foreach (GameObject collider in Colliders)
        {
            collider.GetComponent<DamageScript>().minDmg = 15;
            collider.GetComponent<DamageScript>().maxDmg = 20;
        }
    }


}
