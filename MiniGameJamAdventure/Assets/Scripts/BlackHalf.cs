using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHalf : MonoBehaviour
{
    [SerializeField] private GameObject blackHall;

    private bool _isActivated;

    public Action OnAtcivated;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDestraktable destraktable) && !_isActivated)
        {
            destraktable.Destruct();
            blackHall.SetActive(false);
            _isActivated = true;
            OnAtcivated?.Invoke();
        }
    }
}
