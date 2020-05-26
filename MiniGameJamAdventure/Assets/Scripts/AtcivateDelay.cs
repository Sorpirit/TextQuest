using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtcivateDelay : MonoBehaviour
{
    [SerializeField] private float timeToActivate;

    [SerializeField] private GameObject ObjectToActivate;
    [SerializeField] private GameObject ObjectToDeactivate;
    
    private void Start()
    {
        Invoke(nameof(Active),timeToActivate);
    }

    private void Active()
    {
        if (ObjectToActivate != null) ObjectToActivate.SetActive(true);
        if (ObjectToDeactivate != null) ObjectToDeactivate.SetActive(false);
    }
}