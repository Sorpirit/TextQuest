using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHit : MonoBehaviour
{
    [SerializeField] private float hitDistance;
    [SerializeField] private float hitRate;
    [SerializeField] private GameObject effector;
    [SerializeField] private Collider2D hurtCollider;
    [SerializeField] private Transform hand;
    [SerializeField] private ContactFilter2D playerFilter;
    [SerializeField] private float damage;
    [SerializeField] private GameObject hitEffectPrefab;
    
    private IDirectable _directable;
    private float attackTimer;
    private DamageInfo info;

    private void Awake()
    {
        _directable = GetComponent<IDirectable>();
        info = new DamageInfo();
        info.damage = damage;
        info.dealer = gameObject;
    }

    private void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        
        if(_directable.FoundTarget == null)
            return;

        hand.up = _directable.Direction.normalized;
        
        float ditance = Vector2.Distance(_directable.FoundTarget.position, transform.position);
        if (ditance <= hitDistance && attackTimer <= 0)
        {
            Attack();
            attackTimer = hitRate;
        }
    }

    private void Attack()
    {
        List<Collider2D> hits = new List<Collider2D>();
        hurtCollider.OverlapCollider(playerFilter,hits);
        foreach (var target in hits)
        {
            if (target.CompareTag("Player") && target.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(info);
                Instantiate(hitEffectPrefab, target.transform.position, hand.rotation);
            }
        }
        StartCoroutine(ShootEffector());
    }
    
    private IEnumerator ShootEffector()
    {
        effector.SetActive(true);
        yield return new WaitForSeconds(.1f);
        effector.SetActive(false);
    }
}
