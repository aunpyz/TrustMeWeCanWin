﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer1;
    public bool isPlayer2;
    private float cooldownAttackCounter;
    [SerializeField]
    private float delayBoforeAttackTime;
    private float delayBoforeAttackCounter;
    private bool isDelayBeforeAttackCounting;
    private bool isP1Attack;
    private bool isP2Attack;
    [Header("MaiKeaw")]
    [SerializeField] private GameObject HPBarUI;
    [SerializeField] private BossController theBoss;
    [SerializeField] private GameObject BloodEffect;
    [SerializeField] private CameraController theCamera;
    public Animator P1AttackAnimator;
    public Animator P2AttackAnimator;
    [SerializeField] private SpriteRenderer PlayerBody;
    [SerializeField] private SpriteRenderer PlayerHand;
    [SerializeField] private GameObject GraveObject;
    public Transform P1AttackBloodSpawn;
    public Transform P2AttackBloodSpawn;
    [SerializeField] AudioSource AttackSound;
    [SerializeField] AudioSource AttackSound2;

    [Header("Cheat")]
    [SerializeField] private int currentHP;
    public int CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value > maxHP ? maxHP : value; }
    }

    [Header("Player Setting")]
    [SerializeField] private int maxHP;
    public float cooldownAttackTime;
    [SerializeField] private int P1Damage;
    [SerializeField] private int P2Damage;
    public int Damage
    {
        get
        {
            if (isPlayer1) return P1Damage;
            else return P2Damage;
        }
        set
        {
            if (isPlayer1) P1Damage = value;
            else P2Damage = value;
        }
    }

    [HideInInspector]
    public bool isDeath;

    [Header("Player's item")]
    public ItemContainer itemContainer;
    [SerializeField] private PlayerController friend;
    [SerializeField] private string playerName;

    void Start()
    {
        InitPlayerName();
        maxHP = 10;
        currentHP = maxHP;
    }

    void Update()
    {
        #region   //Controller
        if (Input.anyKey)
        {
            if (isPlayer1 && !isDeath)
            {
                if (Input.GetButtonDown("P1Attack") && !isP1Attack)
                {
                    AttackSound.Play();
                    P1AttackAnimator.SetTrigger("P1Attack");
                    isDelayBeforeAttackCounting = true;
                    delayBoforeAttackCounter = delayBoforeAttackTime;
                }

                if (Input.GetButtonDown("P1UseItem") && !itemContainer.Empty)
                {
                    ConsumeItem();
                }

                if (Input.GetButtonDown("P1NextItem"))
                {
                    itemContainer.MoveSelected();
                }
            }
            if (isPlayer2 && !isDeath)
            {
                if (Input.GetButtonDown("P2Attack") && !isP2Attack)
                {
                    AttackSound2.Play();
                    P2AttackAnimator.SetTrigger("P2Attack");
                    isDelayBeforeAttackCounting = true;
                    delayBoforeAttackCounter = delayBoforeAttackTime;
                }

                if (Input.GetButtonDown("P2UseItem") && !itemContainer.Empty)
                {
                    ConsumeItem();
                }

                if (Input.GetButtonDown("P2NextItem"))
                {
                    itemContainer.MoveSelected();
                }
            }
            if (Input.GetButtonDown("Restart"))
            {
                RestartGame();
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
        theBoss.AttackBoss(playerName, P1Damage, P1AttackBloodSpawn.position);
        //Instantiate(BloodEffect, P1AttackBloodSpawn.position, Quaternion.identity);
    }

    void P2AttackBoss()
    {
        isDelayBeforeAttackCounting = false;
        isP2Attack = true;
        theCamera.CameraShake();
        cooldownAttackCounter = cooldownAttackTime;
        theBoss.AttackBoss(playerName, P2Damage, P2AttackBloodSpawn.position);
        //Instantiate(BloodEffect, P2AttackBloodSpawn.position, Quaternion.identity);
    }

    public void UpdatePlayerHPBar()
    {
        float hp_ratio = (float)currentHP / (float)maxHP;
        if (hp_ratio >= 0)
            HPBarUI.transform.localScale = new Vector3(hp_ratio, 1, 1);
        else
            HPBarUI.transform.localScale = new Vector3(0, 1, 1);
    }

    public void DecreasHP(int? damage)
    {
        currentHP -= damage ?? 1;
        UpdatePlayerHPBar();
        if (currentHP <= 0)
            PlayerDeath();
    }

    void PlayerDeath()
    {
        isDeath = true;
        PlayerBody.enabled = false;
        PlayerHand.enabled = false;
        GraveObject.SetActive(true);
    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main");
    }


    public void AddItem(Item item)
    {
        itemContainer.AddItem(item);
    }

    public void RemoveItem(Item item)
    {
        itemContainer.RemoveItem(item);
    }

    public void ConsumeItem()
    {
        try
        {
            var item = itemContainer.PullSelected();
            item.Consume(this, friend);
        }
        catch
        {
            Debug.LogError("item is null");
        }
    }

    public void InitPlayerName(bool inverse = false)
    {
        playerName = inverse ? isPlayer1 ? "Player2" : "Player1"
                            : isPlayer1 ? "Player1" : "Player2";
    }
}
