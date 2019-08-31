using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemEffectCallback();
public delegate void AttackHandler(int? dmg);

public class CharacterController : MonoBehaviour
{
    private static CharacterController _instance;
    public static CharacterController Instance
    {
        get { return _instance; }
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

    public IEnumerator Buff(ItemEffectCallback callback, float cooldownTime,
        ItemEffectCallback afterCallback)
    {
        callback();
        yield return new WaitForSeconds(cooldownTime);
        afterCallback();
    }
}
