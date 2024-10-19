using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerWagon002 : MonoBehaviour
{
    public GameObject _archer;

    private Animator _animator;

    private void Start()
    {
        _animator = _archer.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetTrigger("trigger");
        }
    }
}
