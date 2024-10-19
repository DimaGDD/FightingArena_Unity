using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Transform _center;
    private GameObject _arena;
    private ControllFights _arenaScript;

    public float radius = 50f;

    private float _rotationSpeed = 5;
    public float _attackRadius = 0.5f;
    public int HP = 100;
    public float moveSpeed = 100f;

    public Animator animator;
    public Slider healthBar;
    public Rigidbody _rb;
    public GameObject _weaponCollider;

    private bool _isWaitingForAttack = false;
    public int _destroyDelay = 5;
    public bool _isDead = false;

    private void Start()
    {
        _arena = GameObject.Find("ControllFight");
        _arenaScript = _arena.GetComponent<ControllFights>();
    }

    private void FixedUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var player in players)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = playerPosition - transform.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

            if (direction.sqrMagnitude <= _attackRadius * _attackRadius)
            {
                if (_isWaitingForAttack == false)
                {
                    animator.SetBool("isAttack", true);
                    _isWaitingForAttack = true;
                }
            }
            else if (direction.magnitude > _attackRadius)
            {
                _rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
                animator.SetBool("isWalk", true);
                animator.SetBool("isAttack", false);
            }
        }
    }

    public void FinishAnimationAttack()
    {
        animator.SetBool("isAttack", false);
        _isWaitingForAttack = false;
    }
    void Update()
    {
        healthBar.value = HP;
    }
 
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            animator.SetBool("isAttack", false);
            animator.SetTrigger("death");
            gameObject.GetComponent<Collider>().enabled = false;
            healthBar.gameObject.SetActive(false);
            gameObject.tag = "Death";
            _rb.isKinematic = true;
            gameObject.GetComponent<EnemyScript>().enabled = false;
            _isDead = true;
            _arenaScript.IsDead(gameObject);


        }   
        else
        {
            animator.SetTrigger("damage");

            StartCoroutine(MoveBackwardOverTime(0.5f));
        }
    }

    private IEnumerator MoveBackwardOverTime(float duration)
    {
        Vector3 backwardForce = transform.forward * -4f;
        _rb.AddForce(backwardForce, ForceMode.Impulse);
        yield return new WaitForSeconds(duration);
        _rb.velocity = Vector3.zero;
    }

    public void IsHitEnd()
    {
        _isWaitingForAttack = false;
    }

    public void ActivateWeaponCollider(string attack)
    {
        _weaponCollider.SetActive(true);
    }

    public void DeactivateWeaponCollider(string attack)
    {
        _weaponCollider.SetActive(false);
    }
}
