using System;
using UnityEngine;

public class RockGrabeable : MonoBehaviour,IGraberable,IDestraktable
{

    private Rigidbody2D _rigidbody2D;
    
    public Rigidbody2D body
    {
        get => _rigidbody2D;
    }
    public bool isFixed => true;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public bool Grab(GameObject graber)
    {
        Debug.Log("Grab Rock");
        return true;
    }

    public bool Drop(GameObject graber)
    {
        Debug.Log("Drop Rock");
        return true;
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }
}