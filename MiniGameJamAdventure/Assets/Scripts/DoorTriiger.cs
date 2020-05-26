using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriiger : MonoBehaviour
{
    [SerializeField] private Animator dorAnimator;

    private int grabables;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IGraberable graberable))
        {
            if(grabables <= 0)
                dorAnimator.SetTrigger("Trigger");
            grabables++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IGraberable graberable))
        {
            grabables--;
            if(grabables <= 0)
                dorAnimator.SetTrigger("OutTrigger");
        }
    }
}
