using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{

    public GameObject _dealer;

    private Animator _animator;

    private void Start()
    {
        _animator = _dealer.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetTrigger("trigger");
        }
    }
}
