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
    [SerializeField]  private TextMeshProUGUI _useText;
    [SerializeField] private GameObject _info;
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _textStage;
    [SerializeField] private TextMeshProUGUI _awards;
    [SerializeField] private GameObject[] _ui;
    [SerializeField] private GameObject _pause;
    [SerializeField] private Transform _targetPositionInArena;
    [SerializeField] private GameObject _stopWall;

    public float radius = 50f;

    public int numberOfStage = 1;

    private bool _showInfo;
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
        if (IsInRadius() && !_showInfo && !_ControllFight.GetComponent<ControllFights>().inArena && !_ControllFight.GetComponent<ControllFights>().deathInfo && !_pause.GetComponent<pausebutton>().isPause)
        {
            _use.SetActive(true);
            _useText.text = "Войти";
        }
        else
        {
            _useText.text = "";
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

        //_stopWall.GetComponent<Collider>().enabled = false;
        //Vector3 direction = _targetPositionInArena.position - _player.GetComponent<PlayerMovementAndroid>()._rb.position;
        //_player.GetComponent<PlayerMovementAndroid>()._rb.AddForce(direction.normalized * 30, ForceMode.Impulse);
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
