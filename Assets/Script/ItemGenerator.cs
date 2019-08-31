using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SRandom = System.Random;

public enum Type
{
    Necro,
    TradeMate,
    Slow,
    ReflectShield,
    HPPotion,
    DivideShield,
    Storm,
    Confuser
}

public static class Helper
{
    static SRandom rnd = new SRandom();

    public static T Random<T>(this List<T> self)
    {
        // return self[rnd.Next(self.Count)];
        return self[7];
    }
}

[System.Serializable]
public class ItemType
{
    public string name;
    public string description;
    public Type type;
}

public class ItemGenerator : MonoBehaviour
{
    public Item item;
    public List<ItemType> itemTypes;
    public BossController boss;

    private static ItemGenerator _instance;
    public static ItemGenerator Instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Item GenerateItem()
    {
        var itemType = itemTypes.Random();
        Item droppedItem = Instantiate(item).Init(itemType.name, itemType.description);
        #region All items' effect
        switch (itemType.type)
        {
            case Type.Necro:
                droppedItem.SetEffect(
                    (PlayerController self, PlayerController friend) =>
                    {
                        if (!friend.isDeath)
                        {
                            var originalDamage = self.Damage;
                            friend.DecreasHP(3);
                            StartCoroutine(CharacterController.Instance.Buff(
                                () => { self.Damage = originalDamage * 2; },
                                6f, () => { self.Damage = originalDamage; }
                            ));
                        }
                    }
                );
                break;
            case Type.TradeMate:
                droppedItem.SetEffect(
                        (PlayerController self, PlayerController friend) =>
                        {
                            if (!friend.isDeath)
                            {
                                var playerHp = self.CurrentHP;
                                self.CurrentHP = friend.CurrentHP;
                                friend.CurrentHP = playerHp;

                                self.UpdatePlayerHPBar();
                                friend.UpdatePlayerHPBar();
                            }
                        }
                    );
                break;
            case Type.Slow:
                droppedItem.SetEffect(
                        (PlayerController self, PlayerController friend) =>
                        {
                            var friendCooldown = friend.cooldownAttackTime;
                            var bossCooldown = boss.bossCooldownAttackTime;
                            StartCoroutine(CharacterController.Instance.Buff(() =>
                            {
                                friend.cooldownAttackTime *= 3;
                                boss.bossCooldownAttackTime *= 3;
                            }, 6f, () =>
                            {
                                friend.cooldownAttackTime = friendCooldown;
                                boss.bossCooldownAttackTime = bossCooldown;
                            }));
                        }
                    );
                break;
            case Type.ReflectShield:
                droppedItem.SetEffect(
                        (PlayerController self, PlayerController friend) =>
                        {
                            if (!friend.isDeath)
                            {
                                if (self.isPlayer1)
                                {
                                    // friend takes 1 hit to player
                                    boss.p1 = (int? damage) =>
                                    {
                                        boss.BossAttackP2(null);
                                        boss.ResetAttackHandler();
                                    };
                                }
                                else
                                {
                                    boss.p2 = (int? damage) =>
                                    {
                                        boss.BossAttackP1(null);
                                        boss.ResetAttackHandler();
                                    };
                                }
                            }
                        }
                    );
                break;
            case Type.HPPotion:
                droppedItem.SetEffect(
                    (PlayerController self, PlayerController friend) =>
                    {
                        self.CurrentHP += 3;
                        self.UpdatePlayerHPBar();
                    }
                );
                break;
            case Type.DivideShield:
                droppedItem.SetEffect(
                        (PlayerController self, PlayerController friend) =>
                        {
                            var bossDmg = boss.bossDamage;
                            StartCoroutine(CharacterController.Instance.Buff(
                                () => { boss.bossDamage = 0; },
                                6f, () => { boss.bossDamage = bossDmg; }
                            ));
                        }
                    );
                break;
            case Type.Storm:
                droppedItem.SetEffect(
                        (PlayerController self, PlayerController friend) =>
                        {
                            const int damage = 3;
                            boss.BossAttackP1(damage);
                            boss.BossAttackP2(damage);
                            boss.AttackBoss(boss.Facing, damage);
                        }
                    );
                break;
            case Type.Confuser:
                droppedItem.SetEffect(
                        (PlayerController self, PlayerController friend) =>
                        {
                            StartCoroutine(CharacterController.Instance.Buff(
                                () =>
                                {
                                    self.InitPlayerName(true);
                                    friend.InitPlayerName(true);
                                }, 6f, () =>
                                {
                                    self.InitPlayerName();
                                    friend.InitPlayerName();
                                }
                            ));
                        }
                    );
                break;
        }
        #endregion
        return droppedItem;
    }
}
