using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator playerAnimator; 

    private Rigidbody2D _moveRb;
    private Vector2 _moveDir;
    private bool isMoving;

    private void Awake()
    {
        _moveRb = GetComponent<Rigidbody2D>();
        playerAnimator.SetFloat("HorDir",.1f);
    }

    private void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");
        if(Math.Abs(_moveDir.x) > 0)
            playerAnimator.SetFloat("HorDir",_moveDir.x);
        _moveDir.Normalize();
        if(isMoving == (_moveDir == Vector2.zero))
            playerAnimator.SetBool("IsMoveing",!isMoving);
        isMoving = _moveDir.magnitude > 0;
    }

    private void Move()
    {
        _moveRb.velocity = _moveDir * speed;
    }
}
