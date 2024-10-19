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
    private float _attackRadius = 1;
    public int HP = 100;
    public float moveSpeed = 100f;

    public Animator animator;
    public Slider healthBar;
    public Rigidbody _rb;
    public GameObject _weaponCollider;

    private bool _isWaitingForAttack;
    private float _attackDelay = 2f;
    public int _destroyDelay = 5;
    private bool _isHit = false;
    public bool _isDead = false;

    private void Start()
    {
        _arena = GameObject.Find("ControllFight");
        _arenaScript = _arena.GetComponent<ControllFights>();
    }

    private void FixedUpdate()
    {
        if (_isHit == false)
        {
            if (IsInRadius())
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject player in players)
                {
                    Vector3 playerPosition = player.transform.position;
                    Vector3 direction = playerPosition - transform.position;
                    direction.y = 0;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

                    if (direction.magnitude <= _attackRadius)
                    {
                        if (!_isWaitingForAttack)
                        {
                            StartCoroutine(AttackWithDelay(_attackDelay));
                            return;
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
            else
            {
                animator.SetBool("isWalk", false);
                animator.SetBool("isAttack", false);
            }
        }
    }

    private IEnumerator AttackWithDelay(float delay)
    {
        _isWaitingForAttack = true;

        yield return new WaitForSeconds(delay);

        animator.SetBool("isAttack", true);
        Invoke("ResetAttack", _attackDelay);
    }

    private void ResetAttack()
    {
        _isWaitingForAttack = false;
    }

    public void FinishAnimationAttack()
    {
        animator.SetBool("isAttack", false);
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
            animator.SetTrigger("death");
            gameObject.GetComponent<Collider>().enabled = false;
            healthBar.gameObject.SetActive(false);
            gameObject.tag = "Death";
            _rb.isKinematic = true;
            gameObject.GetComponent<EnemyScript>().enabled = false;
            _isDead = true;
            _arenaScript.IsDead();
            StartCoroutine(DestroyObjectWithDelay());


        }   
        else
        {
            animator.SetTrigger("damage");
            _isHit = true;

            StartCoroutine(MoveBackwardOverTime(0.5f));
        }
    }

    private IEnumerator DestroyObjectWithDelay()
    {
        if (_isDead == true)
        {
            GameObject objectToDestroy = GameObject.FindWithTag("Death");
            yield return new WaitForSeconds(_destroyDelay);
            Destroy(objectToDestroy);
        }
    }

    private IEnumerator MoveBackwardOverTime(float duration)
    {
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition - transform.forward * 1f;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    public void IsHitEnd()
    {
        _isHit = false;
    }

    private bool IsInRadius()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = playerPosition - _center.position;

            if (direction.magnitude <= radius)
                return true;
        }

        return false;
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
