using System;
using UnityEngine;

public class SimpleHealth : MonoBehaviour,IDamageable
{
    
    [SerializeField] private float maxHp;
    
    public float Hp
    {
        get => _curentHp;
        set
        {
            _curentHp = value;
            if(_curentHp <= 0)
                Die();
            _curentHp = Mathf.Clamp(_curentHp, 0, maxHp);
        }
    }

    private float _curentHp;

    private void Awake()
    {
        _curentHp = maxHp;
    }

    public void TakeDamage(DamageInfo info)
    {
        if(info.dealer == gameObject)
            return;
        
        Hp -= info.damage;
    }

    private void Die()
    {
        Destroy(transform.gameObject);
    }
}