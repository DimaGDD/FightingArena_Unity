using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerMovementAndroid : MonoBehaviour
{
    public float delay1 = 0.9f;
    public float delay2 = 1.2f;
    public float delay3 = 2.9f;
    public float delay4 = 2.8f;

    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] public Rigidbody _rb;
    [SerializeField] public Animator _animator;
    [SerializeField] private GameObject _weaponCollider;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Transform _center;
    [SerializeField] private GameObject _attackButton;
    [SerializeField] private GameObject _dodgeButton;
    [SerializeField] private GameObject[] _ui;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private GameObject _controllFight;
    [SerializeField] private GameObject DamageUp;
    [SerializeField] private GameObject CritUp;
    [SerializeField] private GameObject useLongHeal;

    public float radius = 50f;

    [SerializeField] private float playerSpeed = 100f;
    [SerializeField] private float playerSpeedCombat = 100f;
    [SerializeField] private float rotationSpeed = 5f;

    bool _isAttacking;
    private bool isMoving = false;
    float currentWalkSpeed;

    public float health { get; set; }
    public float maxHealth = 100;
    public GameObject player;
    public Slider healthSlider;
    public GameObject deathScreen;



    public float comboDelay = 1f;
    private DateTime _lastAttackTime;
    private float _secondsSinceLastAttack;
    private float _delay;
    private int numberAnimation;
    private bool _isHit = false;

    public int money { get; set; }

    AudioManager audioManager;


    private void Awake()
    {
        Time.timeScale = 1.0f;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        money = PlayerPrefs.GetInt("SavedMoney", 0);

        health = PlayerPrefs.GetFloat("SavedHealth", maxHealth);
        healthSlider.value = CalculateHealthPercentage();
        _joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
    }

    float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }

    public void UpdateHealthSlider()
    {
        healthSlider.value = CalculateHealthPercentage();
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        healthSlider.value = CalculateHealthPercentage();
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }


    private void FixedUpdate()
    {
        if (_isHit == false)
        {
            if (IsInRadius())
            {
                if (!_isAttacking)
                {
                    Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                    Vector3 movementDirection = _joystick.Direction.x * Camera.main.transform.right + _joystick.Direction.y * cameraForward;
                    Vector3 movement = movementDirection * playerSpeedCombat * Time.fixedDeltaTime;
                    _rb.velocity = new Vector3(movement.x, _rb.velocity.y, movement.z);


                    if (movementDirection != Vector3.zero)
                    {
                        Quaternion newRotation = Quaternion.LookRotation(movementDirection);
                        _rb.rotation = Quaternion.Slerp(_rb.rotation, newRotation, 0.15f);
                    }

                    currentWalkSpeed = movement.magnitude / Time.fixedDeltaTime;
                }
                else
                {
                    GameObject closestEnemy = FindClosestEnemy();

                    if (closestEnemy != null)
                    {
                        //Debug.Log("Attacking");
                        Vector3 directionToEnemy = closestEnemy.transform.position - transform.position;
                        directionToEnemy.y = 0;

                        Quaternion newRotation = Quaternion.LookRotation(directionToEnemy);
                        _rb.rotation = Quaternion.Slerp(_rb.rotation, newRotation, rotationSpeed * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (!_isAttacking)
                {
                    Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                    Vector3 movementDirection = _joystick.Direction.x * Camera.main.transform.right + _joystick.Direction.y * cameraForward;
                    Vector3 movement = movementDirection * playerSpeed * Time.fixedDeltaTime;
                    _rb.velocity = new Vector3(movement.x, _rb.velocity.y, movement.z);

                    if (movementDirection != Vector3.zero)
                    {
                        Quaternion newRotation = Quaternion.LookRotation(movementDirection);
                        _rb.rotation = Quaternion.Slerp(_rb.rotation, newRotation, 0.15f);
                    }

                    currentWalkSpeed = movement.magnitude / Time.fixedDeltaTime;
                }
                else
                {

                }
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        var enemies = ControllFights.Instance.allEnemies;

        if (enemies.Count == 0)
            return null;

        var closestEnemy = enemies.OrderBy(enemy => Vector3.Distance(Camera.main.transform.position, enemy.transform.position)).FirstOrDefault();

        return closestEnemy;
    }

    private bool IsInRadius()
    {
        var enemies = ControllFights.Instance.allEnemies;

        foreach (GameObject enemy in enemies)
        {
            Vector3 enemyPosition = enemy.transform.position;
            Vector3 direction = enemyPosition - _center.position;

            if (direction.sqrMagnitude <= radius * radius)
                return true;
        }

        return false;
    }

    private void Update()
    {
        _money.text = "Деньги: " + money;

        if (!IsInRadius())
        {
            _dodgeButton.GetComponent<Button>().enabled = false;
            _attackButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            _dodgeButton.GetComponent<Button>().enabled = true;
            _attackButton.GetComponent<Button>().enabled = true;
        }

        TimeSpan timeSinceLastAttack = DateTime.Now - _lastAttackTime;
        _secondsSinceLastAttack = (float)timeSinceLastAttack.TotalSeconds;

        if (_secondsSinceLastAttack > _delay)
        {
            _isAttacking = false;
        }

        
        Animations();

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            if (!isMoving && !_isAttacking)
            {
                audioManager.PlayWalkingSound();
                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
            {
                audioManager.StopSFX();
                isMoving = false;
            }
        }

    }

    private void Animations()
    {
        if (IsInRadius())
        {
            _weapon.SetActive(true);
            _animator.SetLayerWeight(1, Mathf.MoveTowards(_animator.GetLayerWeight(1), 1, Time.deltaTime * 2f));
            _animator.SetLayerWeight(0, Mathf.MoveTowards(_animator.GetLayerWeight(0), 0, Time.deltaTime * 2f));

        }
        else
        {
            _weapon.SetActive(false);
            _animator.SetLayerWeight(0, Mathf.MoveTowards(_animator.GetLayerWeight(0), 1, Time.deltaTime * 2f));
            _animator.SetLayerWeight(1, Mathf.MoveTowards(_animator.GetLayerWeight(1), 0, Time.deltaTime * 2f));
        }

        if (currentWalkSpeed == 0 && _isAttacking == false)
        {
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun", false);
        }
        else if (currentWalkSpeed > 0 && currentWalkSpeed <= playerSpeed * 0.7 && _isAttacking == false)
        {
            _animator.SetBool("isWalk", true);
            _animator.SetBool("isRun", false);
            audioManager.SetWalkingPitch(0.75f);
        }
        else if (currentWalkSpeed > playerSpeed * 0.7 && currentWalkSpeed <= playerSpeed && _isAttacking == false)
        {
            _animator.SetBool("isRun", true);
            _animator.SetBool("isWalk", false);
            audioManager.SetWalkingPitch(1f);
        }
    }

    public void DodgeButton()
    {
        if (_isHit == false)
        {
            audioManager.PlayDodgeSound();
            _animator.SetTrigger("isDodge");
            _animator.SetBool("isAttack", false);
            _animator.SetBool("isAttack2", false);
            _animator.SetBool("isAttack3", false);

            _delay = 0;

            //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;                         //
            //Vector3 movementDirection = _joystick.Direction.x * Camera.main.transform.right + _joystick.Direction.y * cameraForward;       // Увороты в сторону, куда смотрит персонаж
            //StartCoroutine(MoveBackwardOverTimeDodge(0.2f, -1f, -movementDirection.normalized));                                           //

            StartCoroutine(MoveBackwardOverTime(0.4f, -5f));
            gameObject.GetComponent<PlayerMovementAndroid>().enabled = false;
            _dodgeButton.GetComponent<Button>().enabled = false;
        }
    }

    //private IEnumerator MoveBackwardOverTimeDodge(float duration, float distance, Vector3 dodgeDirection)
    //{
    //    Vector3 initialPosition = transform.position;
    //    Vector3 targetPosition = initialPosition + dodgeDirection * distance;

    //    float elapsed = 0f;

    //    while (elapsed < duration)
    //    {
    //        transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / duration);

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    transform.position = targetPosition;

    //    gameObject.GetComponent<PlayerMovementAndroid>().enabled = true;
    //    _dodgeButton.GetComponent<Button>().enabled = true;
    //}

    public void FinishDodgeAnimation()
    {
        gameObject.GetComponent<PlayerMovementAndroid>().enabled = true;
        _dodgeButton.GetComponent<Button>().enabled = true;
        _attackButton.GetComponent<Button>().enabled = true;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthSlider.value = CalculateHealthPercentage();
        

        if (health <= 0)
        {
            audioManager.PlayLoseBattle();
            health = 0;

            _animator.SetBool("isAttack", false);
            _animator.SetBool("isAttack2", false);
            _animator.SetBool("isAttack3", false);

            DamageUp.GetComponent<dmgUpgradeButton>().ResetDmg();
            CritUp.GetComponent<critChanceButtonClick>().ResetCritChance();
            gameObject.tag = "Death";
            _animator.SetTrigger("isDead");
            //gameObject.GetComponent<Collider>().enabled = false;
            //healthBar.gameObject.SetActive(false);
            //_rb.isKinematic = true;
            gameObject.GetComponent<PlayerMovementAndroid>().enabled = false;

            //if (!deathScreen.activeSelf)
            //{
            //    deathScreen.SetActive(true);
            //}

            Time.timeScale = 0.5f;
            
            for (int i = 0; i < _ui.Length; i++)
            {
                _ui[i].gameObject.SetActive(false);
            }

            Invoke("CallPlayerDead", 2f);
        }
        else
        {
            _animator.SetTrigger("takeDamage");
            _attackButton.GetComponent<Button>().enabled = false;
            _dodgeButton.GetComponent<Button>().enabled = false;
            _isHit = true;

            StartCoroutine(MoveBackwardOverTime(0.5f, -4f));

            _animator.SetBool("isAttack", false);
            _animator.SetBool("isAttack2", false);
            _animator.SetBool("isAttack3", false);
        }
    }

    private void CallPlayerDead()
    {
        Time.timeScale = 1f;

        for (int i = 0; i < _ui.Length; i++)
        {
            _ui[i].gameObject.SetActive(true);
        }

        health = 100;
        healthSlider.value = 100;
        gameObject.tag = "Player";
        //gameObject.GetComponent<Collider>().enabled = true;
        //_rb.isKinematic = false;
        gameObject.GetComponent<PlayerMovementAndroid>().enabled = true;

        _controllFight.GetComponent<ControllFights>().PlayerDead();
    }

    private IEnumerator MoveBackwardOverTime(float duration, float distance)
    {
        Vector3 backwardForce = transform.forward * distance;
        _rb.AddForce(backwardForce, ForceMode.Impulse);
        yield return new WaitForSeconds(duration);
        _rb.velocity = Vector3.zero;
    }

    public void IsHitEnd()
    {
        _isHit = false;
        gameObject.GetComponent<PlayerMovementAndroid>().enabled = true;
        _attackButton.GetComponent<Button>().enabled = true;
        _dodgeButton.GetComponent<Button>().enabled = true;
    }

    public void FinishAttack1()
    {
        _animator.SetBool("isAttack", false);
        gameObject.GetComponent<PlayerMovementAndroid>().enabled = true;
    }

    public void FinishAttack2()
    {
        _animator.SetBool("isAttack2", false);
        gameObject.GetComponent<PlayerMovementAndroid>().enabled = true;
    }

    public void FinishAttack3()
    {
        _animator.SetBool("isAttack3", false);
        gameObject.GetComponent<PlayerMovementAndroid>().enabled = true;
    }

    public void StartAttack1()
    {
        StartCoroutine(MoveBackwardOverTime(0.5f, 1f));
    }

    public void StartAttack2()
    {
        StartCoroutine(MoveBackwardOverTime(0.5f, 1.5f));
    }

    public void StartAttack3()
    {
        StartCoroutine(MoveBackwardOverTime(0.5f, 2f));
    }

    public void AttackButton()
    {
        _isAttacking = true;

        AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);

        foreach (AnimatorClipInfo clip in clipInfo)
        {
            string clipName = clip.clip.name;

            if (clipName != "Attack1.1" && clipName != "Attack1.2" && clipName != "Attack1.3")
            {
                _animator.SetBool("isAttack", true);
                //StartCoroutine(MoveBackwardOverTime(0.4f, 1.5f));
                numberAnimation = 1;
            }
            else if (clipName == "Attack1.1")
            {
                _animator.SetBool("isAttack2", true);
                //StartCoroutine(MoveBackwardOverTime(0, 0));
                numberAnimation = 2;
            }
            else if (clipName == "Attack1.2")
            {
                _animator.SetBool("isAttack3", true);
                //StartCoroutine(MoveBackwardOverTime(0.4f, 1.5f));
                numberAnimation = 3;
            }
            else if (clipName == "Attack1.3")
            {
                _animator.SetBool("isAttack", true);
                _animator.SetBool("isAttack2", false);
                //StartCoroutine(MoveBackwardOverTime(0.4f, 1.5f));
                numberAnimation = 4;
            }
        }

        

        _lastAttackTime = DateTime.Now;
        if (numberAnimation == 1)
            _delay = delay1;
        else if (numberAnimation == 2)
            _delay = delay2;
        else if (numberAnimation == 3)
            _delay = delay3;
        else if (numberAnimation == 4)
            _delay = delay4;

    }


    public void ActivateWeaponCollider()
    {
        _weaponCollider.SetActive(true);
    }

    public void DeactivateWeaponCollider()
    {
        _weaponCollider.SetActive(false);
    }

    public void UseHeal()
    {
        useLongHeal.GetComponent<UseLongHeal>().ActivateStopHealButton();
    }

    public void StopHeal()
    {
        useLongHeal.GetComponent<UseLongHeal>().StopHealing();
    }

    public void Laying()
    {
        useLongHeal.GetComponent<UseLongHeal>().Laying();
    }
}
