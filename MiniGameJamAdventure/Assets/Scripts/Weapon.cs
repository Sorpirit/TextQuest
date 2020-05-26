using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour,IGraberable
{
    [SerializeField] private float attackRate;
    [SerializeField] private float pressDelay;
    [SerializeField] private float damage;
    [SerializeField] private SpriteRenderer inHandSprite;
    [SerializeField] private GameObject onFlorSprite;
    [SerializeField] private Collider2D hurtCollider;
    [SerializeField] private Animator attackAnim;
    [SerializeField] private ContactFilter2D hitFilter;
    [SerializeField] private GameObject hitEffect;
    public Rigidbody2D body => rb;
    public bool isFixed => false;

    private Camera cam;
    private Rigidbody2D rb;
    private bool isActivated;

    private float pressTimer;
    private float attackTimer;
    private bool isAttcking;
    private DamageInfo info;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        info = new DamageInfo {damage = damage, dealer = gameObject};
    }

    private void Update()
    {
        if(!isActivated)
            return;
        
        if (!isAttcking)
        {
            Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.up = (mouseWorldPos -(Vector2) transform.position).normalized;
        }
        

        // float product = Vector2.Dot(Vector2.right,transform.up);
        // if (product > 0 && sprite.flipY)
        // {
        //     sprite.flipY = false;
        // }else if (product < 0 && !sprite.flipY)
        // {
        //     sprite.flipY = true;
        // }
        //
        
        if (pressTimer > 0 && attackTimer <= 0)
        {
            Attack();
            attackTimer = attackRate;
        }

        if (Input.GetMouseButtonDown(0))
            pressTimer = pressDelay;

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
                isAttcking = false;
        }
        if(pressTimer>0)
            pressTimer -= Time.deltaTime;
    }
    
    public bool Grab(GameObject graber)
    {
        isActivated = true;
        onFlorSprite.SetActive(false);
        inHandSprite.gameObject.SetActive(true);
        return true;
    }

    public bool Drop(GameObject graber)
    {
        if (isAttcking)
            return false;
        
        isActivated = false;
        onFlorSprite.SetActive(true);
        inHandSprite.gameObject.SetActive(false);
        return true;
    }

    private void Attack()
    {
        isAttcking = true;
        attackAnim.SetTrigger("Attack");
    }

    public void OnDealDamage()
    {
        List<Collider2D> hits = new List<Collider2D>();
        hurtCollider.OverlapCollider(hitFilter,hits);
        foreach (var target in hits)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(info);
                Instantiate(hitEffect, target.transform.position, hurtCollider.transform.rotation);
            }
        }
    }
}