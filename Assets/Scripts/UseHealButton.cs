using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UseHealButton : MonoBehaviour
{
    public float radius = 50f;
    [SerializeField] private Transform _center;
    [SerializeField] private GameObject _use;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject _npc;
    [SerializeField] private TextMeshProUGUI CostText;

    private Animator _animator;



    public GameObject Effect;

    public bool isPlayerInCollider = false;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        _animator = _npc.GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInCollider = true;

            _animator.SetTrigger("trigger");

            _use.SetActive(true);
            CostText.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInCollider = false;

            _animator.SetTrigger("isGoAway");

            _use.SetActive(false);
            CostText.gameObject.SetActive(false);
        }
    }

    public void Heal()
    {
        PlayerMovementAndroid playerHealth = player.GetComponent<PlayerMovementAndroid>();
        if ((player.GetComponent<PlayerMovementAndroid>().money >= 100) & (playerHealth.health < 100))
        {
            player.GetComponent<PlayerMovementAndroid>().money -= 100;
            playerHealth.HealCharacter(25);
            audioManager.PlayTrueBuy();

            Quaternion spawnRotation = Quaternion.Euler(-32.073f, -204.521f, -276.441f);
            Instantiate(Effect, player.transform.position, spawnRotation);

        }
        else
        {
            audioManager.PlayFalseBuy();
        }
    }
}
