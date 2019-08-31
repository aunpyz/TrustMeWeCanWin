using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private int currentBossHP;
    private int maxBossHP;
    private bool isBossAttack;
    private float bossCooldownAttackCounter;
    private bool isFaceLeft;
    [Header("BloodType")]
    [SerializeField] private GameObject BloodEffect;
    [SerializeField] private GameObject WhiteBloodEffect;
    [SerializeField] private GameObject BrownBloodEffect;
    [SerializeField] private GameObject GreenBloodEffect;
    [SerializeField] private GameObject RainbowBloodEffect;

    [Header("BossComp")]
    [SerializeField] private GameObject BossHPBarUI;
    [SerializeField] private Text BossHPText;
    [SerializeField] private Text BossCountText;
    [SerializeField] private CameraController theCamera;
    [SerializeField] private PlayerController thePlayer1;
    [SerializeField] private PlayerController thePlayer2;
    [SerializeField] private GameObject BossAvatar1;
    [SerializeField] private GameObject BossAvatar2;
    [SerializeField] private GameObject BossAvatar3;
    [SerializeField] private GameObject BossAvatar4;
    [SerializeField] private GameObject BossAvatar5;
    [SerializeField] private GameObject BossAvatar6;
    [SerializeField] private Animator BossAttackAnimation1;
    [SerializeField] private Animator BossAttackAnimation2;
    [SerializeField] private Animator BossAttackAnimation3;
    [SerializeField] private Animator BossAttackAnimation4;
    [SerializeField] private Animator BossAttackAnimation5;
    [SerializeField] private Animator BossAttackAnimation6;
    [SerializeField] private Transform BossBloodSpawnPos1;
    [SerializeField] private Transform BossBloodSpawnPos2;
    [SerializeField] private Transform BossBloodSpawnPos3;
    [SerializeField] private Transform BossBloodSpawnPos4;
    [SerializeField] private Transform BossBloodSpawnPos5;
    [SerializeField] private Transform BossBloodSpawnPos6;

    [Header("Boss Setting")]
    public float bossCooldownAttackTime;
    public int bossDamage;
    [SerializeField] private int maxHPBoss1;
    [SerializeField] private int maxHPBoss2;
    [SerializeField] private int maxHPBoss3;
    [SerializeField] private int maxHPBoss4;
    [SerializeField] private int maxHPBoss5;
    [SerializeField] private int maxHPBoss6;
    private int BossCount;

    void Start()
    {
        BossCount = 1;
        maxBossHP = maxHPBoss1;
        isFaceLeft = true;
        BossReset();
    }

    void Update()
    {

        if (bossCooldownAttackCounter >= 0)
            bossCooldownAttackCounter -= Time.deltaTime;
        else if (bossCooldownAttackCounter < 0)
        {
            isBossAttack = true;
            if (isFaceLeft && !thePlayer1.isDeath)
            {
                BossAttackP1();
            }
            else if (!isFaceLeft && !thePlayer2.isDeath)
            {
                BossAttackP2();
            }
            else if (thePlayer1.isDeath && !thePlayer2.isDeath)
            {
                isFaceLeft = false;
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                BossAttackP2();
            }
            else if (thePlayer2.isDeath && !thePlayer1.isDeath)
            {
                isFaceLeft = true;
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                BossAttackP1();
            }
            else
            {
                Debug.Log("Loos");
            }
            bossCooldownAttackCounter = bossCooldownAttackTime;
        }
    }

    void BossAttackP1()
    {
        theCamera.CameraShake();
        BossAttackAnimation();
        thePlayer1.DecreasHP(bossDamage);
    }

    void BossAttackP2()
    {
        theCamera.CameraShake();
        BossAttackAnimation();
        thePlayer2.DecreasHP(bossDamage);
    }

    void BossAttackAnimation()
    {
        if (BossCount == 1)
        {
            BossAttackAnimation1.SetTrigger("BossAttack");
            Instantiate(BloodEffect, BossBloodSpawnPos1.position, Quaternion.identity);
        }
        else if (BossCount == 2)
        {
            BossAttackAnimation2.SetTrigger("Boss2Attack");
            Instantiate(BloodEffect, BossBloodSpawnPos2.position, Quaternion.identity);
        }
        else if (BossCount == 3)
        {
            Instantiate(BloodEffect, BossBloodSpawnPos3.position, Quaternion.identity);
        }
        else if (BossCount == 4)
        {
            Instantiate(BloodEffect, BossBloodSpawnPos4.position, Quaternion.identity);
        }
        else if (BossCount == 5)
        {
            Instantiate(BloodEffect, BossBloodSpawnPos5.position, Quaternion.identity);
        }
        else if (BossCount == 6)
        {
            BossAttackAnimation6.SetTrigger("Boss6Attack");
            Instantiate(BloodEffect, BossBloodSpawnPos6.position, Quaternion.identity);
        }
    }

    public void AttackBoss(string playerName, int playerDamage, Vector3 bloodPos)
    {
        currentBossHP -= playerDamage;
        if (currentBossHP <= 0)
            BossDeath(playerName);
        else
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
            BossBleed(bloodPos);
            BossAttackedAnimation();
            UpdateBossHPBar();
        }
    }

    void BossBleed(Vector3 bloodPos)
    {
        if (BossCount == 1)
        {
            Instantiate(WhiteBloodEffect, bloodPos, Quaternion.identity);
        }
        else if (BossCount == 2)
        {
            Instantiate(BrownBloodEffect, bloodPos, Quaternion.identity);
        }
        else if (BossCount == 3)
        {
            Instantiate(GreenBloodEffect, bloodPos, Quaternion.identity);
        }
        else if (BossCount == 4)
        {
            Instantiate(GreenBloodEffect, bloodPos, Quaternion.identity);
        }
        else if (BossCount == 5)
        {
            Instantiate(BloodEffect, bloodPos, Quaternion.identity);
        }
        else
        {
            Instantiate(RainbowBloodEffect, bloodPos, Quaternion.identity);
        }
    }

    void BossAttackedAnimation()
    {
        if (BossCount == 1)
        {
            BossAttackAnimation1.SetTrigger("Attacked");
        }
        else if (BossCount == 2)
        {

        }
        else if (BossCount == 3)
        {
        }
        else if (BossCount == 4)
        {
        }
        else if (BossCount == 5)
        {
        }
        else
        {
        }
    }


    void UpdateBossHPBar()
    {
        BossHPText.text = "" + currentBossHP;
        //BossCountText.text = "Boss:" + BossCount;
        UpdateBossName();
        float hp_ratio = (float)currentBossHP / (float)maxBossHP;
        if (hp_ratio >= 0)
            BossHPBarUI.transform.localScale = new Vector3(hp_ratio, 1, 1);
        else
            BossHPBarUI.transform.localScale = new Vector3(0, 1, 1);
    }

    void UpdateBossName()
    {
        if (BossCount == 1)
        {
            BossCountText.text = "Kala";
        }
        else if (BossCount == 2)
        {
            BossCountText.text = "Heeb";
        }
        else if (BossCount == 3)
        {
            BossCountText.text = "Head Knight";
        }
        else if (BossCount == 4)
        {
            BossCountText.text = "???";
        }
        else if (BossCount == 5)
        {
            BossCountText.text = "???";
        }
        else
        {
            BossCountText.text = "The King";
        }
    }

    void BossDeath(string playerName)
    {
        BossAvatar1.SetActive(false);
        BossAvatar2.SetActive(false);
        BossAvatar3.SetActive(false);
        BossAvatar4.SetActive(false);
        BossAvatar5.SetActive(false);
        BossAvatar6.SetActive(false);
        BossCount++;
        if (BossCount == 2)
        {
            BossAvatar2.SetActive(true);
            maxBossHP = maxHPBoss2;
        }
        else if (BossCount == 3)
        {
            BossAvatar3.SetActive(true);
            maxBossHP = maxHPBoss3;
        }
        else if (BossCount == 4)
        {
            BossAvatar4.SetActive(true);
            maxBossHP = maxHPBoss4;
        }
        else if (BossCount == 5)
        {
            BossAvatar5.SetActive(true);
            maxBossHP = maxHPBoss5;
        }
        else if (BossCount == 6)
        {
            BossAvatar6.SetActive(true);
            maxBossHP = maxHPBoss6;
        }
        else
        {
            if (!thePlayer1.isDeath && !thePlayer2.isDeath)
                SceneManager.LoadScene("Victory12");
            else if (!thePlayer1.isDeath)
                SceneManager.LoadScene("Victory1");
            else
                SceneManager.LoadScene("Victory2");
        }
        Item droppedItem = ItemGenerator.Instance.GenerateItem();
        switch (playerName)
        {
            case "Player1":
                thePlayer1.AddItem(droppedItem);
                break;
            case "Player2":
                thePlayer2.AddItem(droppedItem);
                break;
        }
        BossReset();
    }

    void BossReset()
    {
        currentBossHP = maxBossHP;
        UpdateBossHPBar();
        bossCooldownAttackCounter = bossCooldownAttackTime;
        isBossAttack = false;
    }
}
