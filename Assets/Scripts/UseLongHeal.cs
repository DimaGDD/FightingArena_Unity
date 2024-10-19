using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseLongHeal : MonoBehaviour
{
    public GameObject player;
    public GameObject useLongHeal;
    public GameObject stopLongHeal;
    public GameObject Effect;
    private AudioManager _audioManager;
    public int step = -1;
    private float lastUpdateTime = 0.0f;

    private Animator _animator;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        _animator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        if (step == 2 && player.GetComponent<PlayerMovementAndroid>().health < 100)
        {
            float currentTime = Time.time;

            if (currentTime - lastUpdateTime >= 2.0f)
            {
                player.GetComponent<PlayerMovementAndroid>().health += 5;
                player.GetComponent<PlayerMovementAndroid>().UpdateHealthSlider();
                Quaternion spawnRotation = Quaternion.Euler(-28.504f, -206.062f, -270.31f);
                Instantiate(Effect, player.transform.position, spawnRotation);

                lastUpdateTime = currentTime;
            }
        }
        else if (step == 2 && player.GetComponent<PlayerMovementAndroid>().health == 100)
        {
            StopHeal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            useLongHeal.SetActive(true);
            step = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            useLongHeal.SetActive(false);
            step = -1;
        }
    }

    public void UseHeal()
    {
        if (player.GetComponent<PlayerMovementAndroid>().health == 100)
        {
            _audioManager.PlayFalseBuy();
            return;
        }

        player.transform.rotation = new Quaternion(player.transform.rotation.x, 250f, player.transform.rotation.z, player.transform.rotation.w);
        player.GetComponent<PlayerMovementAndroid>().enabled = false;
        _animator.SetTrigger("useHeal");
        useLongHeal.SetActive(false);
        step = 1;
    }

    public void StopHeal()
    {
        _animator.SetTrigger("stopHeal");
        stopLongHeal.SetActive(false);
        step = 3;
    }

    public void ActivateStopHealButton()
    {
        stopLongHeal.SetActive(true);
    }

    public void StopHealing()
    {
        useLongHeal.SetActive(true);
        player.GetComponent<PlayerMovementAndroid>().enabled = true;
        step = 0;
    }

    public void Laying()
    {
        step = 2;
    }

    
}
