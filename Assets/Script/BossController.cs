using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private int currentBossHP;
    private int maxBossHP;
    [SerializeField] private GameObject BossHPBarUI;
    private bool isBossAttack;
    private float bossCooldownAttackCounter;
    public float bossCooldownAttackTime;
    private bool isFaceLeft;
    [SerializeField] private PlayerController thePlayer1;
    [SerializeField] private PlayerController thePlayer2;
    [SerializeField] private GameObject BossAvatar1;
    [SerializeField] private GameObject BossAvatar2;

    void Start()
    {
        maxBossHP = 20;
        currentBossHP = maxBossHP;
        isBossAttack = false;
        isFaceLeft = true;
    }

    void Update()
    {

        if (bossCooldownAttackCounter >= 0)
            bossCooldownAttackCounter -= Time.deltaTime;
        else if (bossCooldownAttackCounter < 0)
        {
            isBossAttack = true;
            if (isFaceLeft)
                thePlayer1.DecreasHP(1);
            else
                thePlayer2.DecreasHP(1);
            bossCooldownAttackCounter = bossCooldownAttackTime;
        }
    }

    public void AttackBoss(string playerName)
    {
        if (playerName.Equals("Player1"))
        {
            isFaceLeft = true;
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (playerName.Equals("Player2"))
        {
            isFaceLeft = false;
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        currentBossHP -= 1;
        UpdateBossHPBar();
        if (currentBossHP == 0)
            BossDeath();
    }

    void UpdateBossHPBar()
    {
        float hp_ratio = (float)currentBossHP / (float)maxBossHP;
        if (hp_ratio >= 0)
            BossHPBarUI.transform.localScale = new Vector3(hp_ratio, 1, 1);
    }

    void BossDeath()
    {
        Debug.Log("BossDead");
    }


}
