using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ControllFights : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private GameObject _winInfo;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _triggerForArena;
    [SerializeField] private GameObject _deathScreenArena;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _awards;
    [SerializeField] private TextMeshProUGUI _stageLoose;
    [SerializeField] private TextMeshProUGUI _youLose;
    [SerializeField] private GameObject[] _ui;
    [SerializeField] private Transform _targetPositionOutOfArena;


    public GameObject enemyPrefab;
    private GameObject spawnedEnemy;
    public Transform[] spawnPoints;

    private bool hasSpawned = false;
    public bool inArena = false;
    public bool win;
    public bool deathInfo;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    //private void OnTriggerStay(Collider other)
    //{
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        _winInfo.SetActive(true);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {

        if (!hasSpawned && other.tag == "Player")
        {
            Vector3 spawnPosition1 = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            spawnPosition1.y = 0.2f;
            spawnedEnemy = Instantiate(enemyPrefab, spawnPosition1, Quaternion.identity);
            hasSpawned = true;
            inArena = true;
            audioManager.PlayBackgroundMusicInBattle();
        }
    }

    public void PlayerDead()
    {
        
        _player.transform.position = _targetPositionOutOfArena.position;

        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(false);
        }

        _deathScreenArena.SetActive(true);
        _stageLoose.text = "Этап " + _triggerForArena.GetComponent<TriggerForArena>().numberOfStage + " не пройден!";
        _youLose.text = "Вы потеряли " + _player.GetComponent<PlayerMovementAndroid>().money;
        _player.GetComponent<PlayerMovementAndroid>().money = 0;
        _triggerForArena.GetComponent<TriggerForArena>().numberOfStage = 1;

        hasSpawned = false;
        inArena = false;
        win = false;
        deathInfo = true;
        audioManager.PlayBackgroundMusic();

        Destroy(spawnedEnemy);
    }

    public void ContinueAfterLoose()
    {
        _deathScreenArena.SetActive(false);

        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(true);
        }

        deathInfo = false;

    }


    public void IsDead()
    {
        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(false);
        }

        audioManager.PlayWinBattle();
        _winInfo.SetActive(true);
        _winText.text = "Вы прошли этап " + _triggerForArena.GetComponent<TriggerForArena>().numberOfStage;
        _awards.text = "Награда: " + (100 * _triggerForArena.GetComponent<TriggerForArena>().numberOfStage);
        _player.GetComponent<PlayerMovementAndroid>().money += (100 * _triggerForArena.GetComponent<TriggerForArena>().numberOfStage);
        _player.transform.position = _targetPositionOutOfArena.position;
        win = false;
        audioManager.PlayBackgroundMusic();
    }

    public void Continue()
    {
        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(true);
        }

        _triggerForArena.GetComponent<TriggerForArena>().numberOfStage++;
        hasSpawned = false;
        inArena = false;
        win = false;
        _winInfo.SetActive(false);
        audioManager.PlayBackgroundMusic();
    }
}
