using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private int currentBossHP;
    private int maxBossHP;
    [SerializeField]
    private bool isBossAttack;
    private float bossCooldownAttackCounter;
    private bool isFaceLeft;
    public string Facing
    {
        get
        {
            if (isFaceLeft) return "Player1";
            else return "Player2";
        }
    }
    public Transform effectTransform;

    [Header("BloodType")]
    [SerializeField] private GameObject BloodEffect;
    [SerializeField] private GameObject WhiteBloodEffect;
    [SerializeField] private GameObject BrownBloodEffect;
    [SerializeField] private GameObject GreenBloodEffect;
    [SerializeField] private GameObject RainbowBloodEffect;
    [SerializeField] private GameObject YellowBloodEffect;
    [SerializeField] private GameObject GreyBloodEffect;
    [SerializeField] private GameObject WheatBloodEffect;

    [Header("BossComp")]
    [SerializeField] private GameObject BossHPBarUI;
    [SerializeField] private Text BossHPText;
    [SerializeField] private Text BossCountText;
    [SerializeField] private CameraController theCamera;
    [SerializeField] private PlayerController thePlayer1;
    [SerializeField] private PlayerController thePlayer2;
    [SerializeField] private GameObject BossAvatar0;
    [SerializeField] private GameObject BossAvatar1;
    [SerializeField] private GameObject BossAvatar2;
    [SerializeField] private GameObject BossAvatar3;
    [SerializeField] private GameObject BossAvatar4;
    [SerializeField] private GameObject BossAvatar5;
    [SerializeField] private GameObject BossAvatar6;
    [SerializeField] private Animator BossAttackAnimation0;
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
    [SerializeField] private AudioSource BossAttackSound1;
    [SerializeField] private AudioSource BossAttackSound2;
    [SerializeField] private AudioSource BossAttackSound3;

    [HideInInspector]
    public int bossDamage;
    [Header("Boss Setting")]
    public float bossCooldownAttackTime;
    [SerializeField] private int maxHPBoss1;
    [SerializeField] private int maxHPBoss2;
    [SerializeField] private int maxHPBoss3;
    [SerializeField] private int maxHPBoss4;
    [SerializeField] private int maxHPBoss5;
    [SerializeField] private int maxHPBoss6;
    [SerializeField] private int bossDamage1;
    [SerializeField] private int bossDamage2;
    [SerializeField] private int bossDamage3;
    [SerializeField] private int bossDamage4;
    [SerializeField] private int bossDamage5;
    [SerializeField] private int bossDamage6;

    [Header("Boss Name")]
    [SerializeField] private string boss0Name;
    [SerializeField] private string boss1Name;
    [SerializeField] private string boss2Name;
    [SerializeField] private string boss3Name;
    [SerializeField] private string boss4Name;
    [SerializeField] private string boss5Name;
    [SerializeField] private string boss6Name;
    private int BossCount;
    public AttackHandler p1;
    public AttackHandler p2;


    void Start()
    {
        bossDamage = bossDamage1;
        ResetAttackHandler();
        BossCount = 0;
        maxBossHP = 9999;
        isFaceLeft = true;
        BossReset();
    }

    void Update()
    {

        if (thePlayer1.isStart || thePlayer2.isStart)
            if (bossCooldownAttackCounter >= 0)
                bossCooldownAttackCounter -= Time.deltaTime;
            else if (bossCooldownAttackCounter < 0)
            {
                isBossAttack = true;
                if (isFaceLeft && !thePlayer1.isDeath)
                {
                    p1(null);
                }
                else if (!isFaceLeft && !thePlayer2.isDeath)
                {
                    p2(null);
                }
                else if (thePlayer1.isDeath && !thePlayer2.isDeath)
                {
                    isFaceLeft = false;
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                    p2(null);
                }
                else if (thePlayer2.isDeath && !thePlayer1.isDeath)
                {
                    isFaceLeft = true;
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                    p1(null);
                }
                else
                {

                }
                //StartCoroutine(DelayBossBeforeAttack());
                bossCooldownAttackCounter = bossCooldownAttackTime;
            }
    }

    IEnumerator DelayBossBeforeAttack()
    {
        yield return new WaitForSeconds(0.25f);
        bossCooldownAttackCounter = bossCooldownAttackTime;
        isBossAttack = false;
    }

    public void BossAttackP1(int? damage)
    {
        theCamera.CameraShake();
        BossAttackSound();
        BossAttackAnimation();
        thePlayer1.P1AttackAnimator.SetTrigger("Attacked");
        thePlayer1.DecreasHP(damage ?? bossDamage);
    }

    public void BossAttackP2(int? damage)
    {
        theCamera.CameraShake();
        BossAttackSound();
        BossAttackAnimation();
        thePlayer2.P2AttackAnimator.SetTrigger("Attacked");
        thePlayer2.DecreasHP(damage ?? bossDamage);
    }

    void BossAttackSound()
    {
        if (BossCount == 1 || BossCount == 2 || BossCount == 4)
        {
            BossAttackSound1.Play();
        }
        else if (BossCount == 5)
        {
            BossAttackSound3.Play();
        }
        else
        {
            BossAttackSound2.Play();
        }
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
            BossAttackAnimation3.SetTrigger("Boss3Attack");
            Instantiate(BloodEffect, BossBloodSpawnPos3.position, Quaternion.identity);
        }
        else if (BossCount == 4)
        {
            BossAttackAnimation4.SetTrigger("Boss4Attack");
            Instantiate(BloodEffect, BossBloodSpawnPos4.position, Quaternion.identity);
        }
        else if (BossCount == 5)
        {
            BossAttackAnimation5.SetTrigger("Boss5Attack");
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
        if (BossCount != 0)
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
        if (BossCount == 0)
        {
            Instantiate(WheatBloodEffect, bloodPos, Quaternion.identity);
        }
        else if (BossCount == 1)
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
            Instantiate(GreyBloodEffect, bloodPos, Quaternion.identity);
        }
        else if (BossCount == 5)
        {
            Instantiate(YellowBloodEffect, bloodPos, Quaternion.identity);
        }
        else
        {
            Instantiate(RainbowBloodEffect, bloodPos, Quaternion.identity);
        }
    }

    void BossAttackedAnimation()
    {
        if (BossCount == 0)
        {
            BossAttackAnimation0.SetTrigger("Attacked");
        }
        else if (BossCount == 1)
        {
            BossAttackAnimation1.SetTrigger("Attacked");
        }
        else if (BossCount == 2)
        {
            BossAttackAnimation2.SetTrigger("Attacked");
        }
        else if (BossCount == 3)
        {
            BossAttackAnimation3.SetTrigger("Attacked");
        }
        else if (BossCount == 4)
        {
            BossAttackAnimation4.SetTrigger("Attacked");
        }
        else if (BossCount == 5)
        {
            BossAttackAnimation5.SetTrigger("Attacked");
        }
        else
        {
            BossAttackAnimation6.SetTrigger("Attacked");
        }
    }

    public void ResetAttackHandler()
    {
        p1 = BossAttackP1;
        p2 = BossAttackP2;
    }

    public void Effect(Type effectType)
    {
        var effectPosition = effectTransform.position;
        Instantiate(ItemGenerator.Instance.ItemEffect(effectType),
                    new Vector3(effectPosition.x, effectPosition.y, -5),
                    Quaternion.identity);
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
        if (BossCount == 0)
        {
            BossCountText.text = boss0Name;
        }
        else if (BossCount == 1)
        {
            BossCountText.text = boss1Name;
        }
        else if (BossCount == 2)
        {
            BossCountText.text = boss2Name;
        }
        else if (BossCount == 3)
        {
            BossCountText.text = boss3Name;
        }
        else if (BossCount == 4)
        {
            BossCountText.text = boss4Name;
        }
        else if (BossCount == 5)
        {
            BossCountText.text = boss5Name;
        }
        else if (BossCount == 6)
        {
            BossCountText.text = boss6Name;
        }
        else
        {
            BossCountText.text = boss6Name;
        }
    }

    public void BossDeath(string playerName)
    {
        BossAvatar0.SetActive(false);
        BossAvatar1.SetActive(false);
        BossAvatar2.SetActive(false);
        BossAvatar3.SetActive(false);
        BossAvatar4.SetActive(false);
        BossAvatar5.SetActive(false);
        BossAvatar6.SetActive(false);
        BossCount++;
        if (BossCount == 1)
        {
            BossAvatar1.SetActive(true);
            maxBossHP = maxHPBoss1;
            bossDamage = bossDamage1;
        }
        else if (BossCount == 2)
        {
            BossAvatar2.SetActive(true);
            maxBossHP = maxHPBoss2;
            bossDamage = bossDamage2;
        }
        else if (BossCount == 3)
        {
            BossAvatar3.SetActive(true);
            maxBossHP = maxHPBoss3;
            bossDamage = bossDamage3;
        }
        else if (BossCount == 4)
        {
            BossAvatar4.SetActive(true);
            maxBossHP = maxHPBoss4;
            bossDamage = bossDamage4;
        }
        else if (BossCount == 5)
        {
            BossAvatar5.SetActive(true);
            maxBossHP = maxHPBoss5;
            bossDamage = bossDamage5;
        }
        else if (BossCount == 6)
        {
            BossAvatar6.SetActive(true);
            maxBossHP = maxHPBoss6;
            bossDamage = bossDamage6;
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

        if (thePlayer1.isStart || thePlayer2.isStart)
        {
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
