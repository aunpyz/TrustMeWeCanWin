using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer1;
    public bool isPlayer2;
    private float cooldownAttackCounter;
    public float cooldownAttackTime;
    private float delayBoforeAttackCounter;
    private bool isDelayBeforeAttackCounting;
    [SerializeField]
    private float delayBoforeAttackTime;
    private bool isP1Attack;
    private bool isP2Attack;
    private int currentHP;
    private int maxHP;
    [SerializeField] private GameObject HPBarUI;
    [SerializeField] private BossController theBoss;
    [SerializeField] private GameObject BloodEffect;
    [HideInInspector]
    public bool isDeath;
    [SerializeField] private CameraController theCamera;
    [SerializeField] private Animator P1AttackAnimator;
    [SerializeField] private Animator P2AttackAnimator;

    void Start()
    {
        maxHP = 10;
        currentHP = maxHP;
    }

    void Update()
    {
        #region   //Controller
        if (Input.anyKey)
        {
            if (isPlayer1 && !isDeath)
                if (Input.GetButtonDown("P1Attack") && !isP1Attack)
                {
                    P1AttackAnimator.SetTrigger("P1Attack");
                    isDelayBeforeAttackCounting = true;
                    delayBoforeAttackCounter = delayBoforeAttackTime;
                }
            if (isPlayer2 && !isDeath)
                if (Input.GetButtonDown("P2Attack") && !isP2Attack)
                {
                    P2AttackAnimator.SetTrigger("P2Attack");
                    isDelayBeforeAttackCounting = true;
                    delayBoforeAttackCounter = delayBoforeAttackTime;
                }
        }
        #endregion

        if (isPlayer1)
        {
            if (isP1Attack)
                if (cooldownAttackCounter > 0)
                    cooldownAttackCounter -= Time.deltaTime;
                else if (cooldownAttackCounter <= 0)
                    isP1Attack = false;

            if (isDelayBeforeAttackCounting)
                if (delayBoforeAttackCounter > 0)
                    delayBoforeAttackCounter -= Time.deltaTime;
                else if (delayBoforeAttackCounter <= 0)
                    P1AttackBoss();
        }

        if (isPlayer2)
        {
            if (isP2Attack)
                if (cooldownAttackCounter > 0)
                    cooldownAttackCounter -= Time.deltaTime;
                else if (cooldownAttackCounter <= 0)
                    isP2Attack = false;

            if (isDelayBeforeAttackCounting)
                if (delayBoforeAttackCounter > 0)
                    delayBoforeAttackCounter -= Time.deltaTime;
                else if (delayBoforeAttackCounter <= 0)
                    P2AttackBoss();
        }
    }

    void P1AttackBoss()
    {
        isDelayBeforeAttackCounting = false;
        isP1Attack = true;
        theCamera.CameraShake();
        cooldownAttackCounter = cooldownAttackTime;
        theBoss.AttackBoss("Player1");
        Instantiate(BloodEffect, new Vector3(transform.position.x + 2, transform.position.y + 1, BloodEffect.transform.position.z), Quaternion.identity);
    }

    void P2AttackBoss()
    {
        isDelayBeforeAttackCounting = false;
        isP2Attack = true;
        theCamera.CameraShake();
        cooldownAttackCounter = cooldownAttackTime;
        theBoss.AttackBoss("Player2");
        Instantiate(BloodEffect, new Vector3(transform.position.x - 2, transform.position.y + 1, BloodEffect.transform.position.z), Quaternion.identity);
    }

    void UpdatePlayerHPBar()
    {
        float hp_ratio = (float)currentHP / (float)maxHP;
        if (hp_ratio >= 0)
            HPBarUI.transform.localScale = new Vector3(hp_ratio, 1, 1);
    }

    public void DecreasHP(int damage)
    {
        currentHP -= 1;
        UpdatePlayerHPBar();
        if (currentHP == 0)
            PlayerDeath();
    }

    void PlayerDeath()
    {
        isDeath = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

}
