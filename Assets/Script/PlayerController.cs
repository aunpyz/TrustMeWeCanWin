using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer1;
    public bool isPlayer2;
    private float cooldownAttackCounter;
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
    [SerializeField] private Animator P1AttackAnimator;
    [SerializeField] private Animator P2AttackAnimator;
    [SerializeField] private SpriteRenderer PlayerBody;
    [SerializeField] private SpriteRenderer PlayerHand;
    [SerializeField] private GameObject GraveObject;
    [SerializeField] private Transform P1AttackBloodSpawn;
    [SerializeField] private Transform P2AttackBloodSpawn;

    [Header("Cheat")]
    [SerializeField] private int currentHP;

    [Header("Player Setting")]
    [SerializeField] private int maxHP;
    public float cooldownAttackTime;
    [SerializeField] private int P1Damage;
    [SerializeField] private int P2Damage;

    [HideInInspector]
    public bool isDeath;

    [SerializeField] private List<Item> items;
    private int currentItemIndex = 0;
    private float itemCooldown = 1f;
    [SerializeField] private PlayerController friend;

    void Start()
    {
        currentHP = maxHP;
        items = new List<Item>(3);
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
                    P1AttackAnimator.SetTrigger("P1Attack");
                    isDelayBeforeAttackCounting = true;
                    delayBoforeAttackCounter = delayBoforeAttackTime;
                }

                if (Input.GetButtonDown("P1UseItem") && items.Count > 0)
                {
                    ConsumeItem();
                }
            }
            if (isPlayer2 && !isDeath)
            {
                if (Input.GetButtonDown("P2Attack") && !isP2Attack)
                {
                    P2AttackAnimator.SetTrigger("P2Attack");
                    isDelayBeforeAttackCounting = true;
                    delayBoforeAttackCounter = delayBoforeAttackTime;
                }

                if (Input.GetButtonDown("P2UseItem") && items.Count > 0)
                {
                    ConsumeItem();
                }
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
        theBoss.AttackBoss("Player1", P1Damage, P1AttackBloodSpawn.position);
        //Instantiate(BloodEffect, P1AttackBloodSpawn.position, Quaternion.identity);
    }

    void P2AttackBoss()
    {
        isDelayBeforeAttackCounting = false;
        isP2Attack = true;
        theCamera.CameraShake();
        cooldownAttackCounter = cooldownAttackTime;
        theBoss.AttackBoss("Player2", P2Damage, P2AttackBloodSpawn.position);
        //Instantiate(BloodEffect, P2AttackBloodSpawn.position, Quaternion.identity);
    }

    void UpdatePlayerHPBar()
    {
        float hp_ratio = (float)currentHP / (float)maxHP;
        if (hp_ratio >= 0)
            HPBarUI.transform.localScale = new Vector3(hp_ratio, 1, 1);
        else
            HPBarUI.transform.localScale = new Vector3(0, 1, 1);
    }

    public void DecreasHP(int bossDamage)
    {
        currentHP -= bossDamage;
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
        if (items.Count < 3)
        {
            items.Add(item);
        }
        else
        {
            items.RemoveAt(0);
            items.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void ConsumeItem()
    {
        var item = items[currentItemIndex];
        RemoveItem(item);
        Debug.Log(item.Name);
        item.Consume(this, friend);
    }
}
