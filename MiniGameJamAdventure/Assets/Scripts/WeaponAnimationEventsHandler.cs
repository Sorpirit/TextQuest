using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEventsHandler : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    public void OnAttack()
    {
        weapon.OnDealDamage();
    }
}
