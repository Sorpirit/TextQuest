using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D _move2D;
    private IDirectable _directable;
    private Vector2 _moveDir;

    private void Start()
    {
        _directable = GetComponent<IDirectable>();
        _move2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveDir = _directable.Direction;
    }

    private void FixedUpdate()
    {
        _move2D.velocity += _moveDir * (speed * Time.fixedDeltaTime);
    }
}
