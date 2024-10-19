using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class TriggerForArena : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private GameObject _use;
    [SerializeField] private GameObject _ControllFight;
    [SerializeField] private TextMeshProUGUI _useText;
    [SerializeField] private GameObject _info;
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _textStage;
    [SerializeField] private TextMeshProUGUI _awards;
    [SerializeField] private GameObject[] _ui;
    [SerializeField] private GameObject _pause;
    [SerializeField] private Transform _targetPositionInArena;
    [SerializeField] private GameObject _stopWall;

    public float radius = 50f;
    public bool isPlayerInCollider = false;

    public int numberOfStage { get; set; }

    private bool _showInfo;

    private void Start()
    {
        numberOfStage = PlayerPrefs.GetInt("SavedStage", 1);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_showInfo && !_ControllFight.GetComponent<ControllFights>().inArena && !_ControllFight.GetComponent<ControllFights>().deathInfo && !_pause.GetComponent<pausebutton>().isPause)
        {
            isPlayerInCollider = true;

            _use.SetActive(true);
            _useText.text = "Войти";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInCollider = false;

            _use.SetActive(false);
        }
    }

    public void ShowInfo()
    {
        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(false);
        }

        _info.SetActive(true);
        _textStage.text = "Этап " + numberOfStage;
        _awards.text = "Награда: " + (100 * numberOfStage);
        _showInfo = true;
    }

    public void StartToArena()
    {
        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(true);
        }

        _player.transform.position = _targetPositionInArena.position;
        _info.SetActive(false);
        _showInfo = false;
    }

    public void CancelToArena()
    {
        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(true);
        }

        _info.SetActive(false);
        _showInfo = false;
    }

}
