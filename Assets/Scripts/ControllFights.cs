using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public List<Transform> spawnPoints;

    private bool hasSpawned = false;
    public bool inArena = false;
    public bool win;
    public bool deathInfo;

    private int numberOfEnemies = 0;
    private int _numberOfStage = 1;
    public List<GameObject> allEnemies = new List<GameObject>();
    Vector3 spawnPosition;
    Transform spawnPositionTransform;
    private List<Transform> _spawnPoints = new List<Transform>();
    
    public static ControllFights Instance { get; private set; }
    AudioManager audioManager;

    Transform colliderAttack1;
    Transform enemyCanvas;
    Transform healthBar;

    private void Awake()
    {   
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSpawned && other.tag == "Player")
        {
            foreach (Transform i in spawnPoints)
            {
                _spawnPoints.Add(i);
            }

            if (_triggerForArena.GetComponent<TriggerForArena>().numberOfStage <= 1)
                _numberOfStage = _triggerForArena.GetComponent<TriggerForArena>().numberOfStage;
            else if (_triggerForArena.GetComponent<TriggerForArena>().numberOfStage > 1)
                _numberOfStage = 1;

            for (int i = 0; i < _numberOfStage; i++)
            {
                if (i < 1)
                {
                    spawnPositionTransform = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];

                    int index = _spawnPoints.IndexOf(spawnPositionTransform);
                    _spawnPoints.RemoveAt(index);

                    spawnPosition = spawnPositionTransform.position;
                    spawnPosition.y = 0.2f;

                    spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                    spawnedEnemy.GetComponent<EnemyScript>().HP += 50 * _triggerForArena.GetComponent<TriggerForArena>().numberOfStage / 2;

                    colliderAttack1 = spawnedEnemy.transform.Find("ColliderAttack1");
                    colliderAttack1.GetComponent<EnemyDamageScript>().damageAmount += 10 * _triggerForArena.GetComponent<TriggerForArena>().numberOfStage / 2;

                    enemyCanvas = spawnedEnemy.transform.Find("EnemyCanvas");
                    healthBar = enemyCanvas.transform.Find("HealthBar");
                    healthBar.GetComponent<Slider>().maxValue += 50 * _triggerForArena.GetComponent<TriggerForArena>().numberOfStage / 2;

                    allEnemies.Add(spawnedEnemy);
                }
            }


            _spawnPoints.Clear();

            if (_spawnPoints.Count == 0)

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

        foreach (GameObject enemy in allEnemies)
        {
            Destroy(enemy);
        }

        allEnemies.Clear();

        numberOfEnemies = 0;
    }

    public void ContinueAfterLoose()
    {
        _deathScreenArena.SetActive(false);

        for (int i = 0; i < _ui.Length; i++)
        {
            if (i != 6)
            {
                _ui[i].SetActive(true);
            }
        }

        deathInfo = false;

    }


    public void IsDead(GameObject enemyGO)
    {
        allEnemies.Remove(enemyGO);
        Destroy(enemyGO, 5f);

        numberOfEnemies++;

        if (numberOfEnemies == _numberOfStage)
        {
            numberOfEnemies = 0;
        }
        else
        {
            return;
        }

        Invoke("CallWaitAfterWin", 3f);
    }

    private void CallWaitAfterWin()
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
            if (i != 6)
            {
                _ui[i].SetActive(true);
            }
        }

        _triggerForArena.GetComponent<TriggerForArena>().numberOfStage++;
        hasSpawned = false;
        inArena = false;
        win = false;
        _winInfo.SetActive(false);
        audioManager.PlayBackgroundMusic();
    }
}