using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer1;
    public bool isPlayer2;
    private float cooldownAttackCounter;
    public float cooldownAttackTime;
    private bool isP1Attack;
    private bool isP2Attack;
    private int currentHP;
    private int maxHP;
    [SerializeField] private GameObject HPBarUI;
    [SerializeField] private BossController theBoss;
    [SerializeField] private GameObject BloodEffect;
    // [HideInInspector]
    public bool isDeath;


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
                    isP1Attack = true;
                    cooldownAttackCounter = cooldownAttackTime;
                    theBoss.AttackBoss("Player1");
                    Instantiate(BloodEffect, new Vector3(transform.position.x + 2, transform.position.y + 1, BloodEffect.transform.position.z), Quaternion.identity);
                }
            if (isPlayer2 && !isDeath)
                if (Input.GetButtonDown("P2Attack") && !isP2Attack)
                {
                    isP2Attack = true;
                    cooldownAttackCounter = cooldownAttackTime;
                    theBoss.AttackBoss("Player2");
                    Instantiate(BloodEffect, new Vector3(transform.position.x - 2, transform.position.y + 1, BloodEffect.transform.position.z), Quaternion.identity);
                }
        }
        #endregion

        if (isPlayer1)
            if (isP1Attack)
                if (cooldownAttackCounter > 0)
                    cooldownAttackCounter -= Time.deltaTime;
                else if (cooldownAttackCounter <= 0)
                    isP1Attack = false;

        if (isP2Attack)
            if (isP2Attack)
                if (cooldownAttackCounter > 0)
                    cooldownAttackCounter -= Time.deltaTime;
                else if (cooldownAttackCounter <= 0)
                    isP2Attack = false;
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

}
