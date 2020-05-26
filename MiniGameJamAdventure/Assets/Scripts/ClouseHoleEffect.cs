using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ClouseHoleEffect : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private BlackHalf half;
    
    private Volume effects;
    private bool isAnimating;

    private void Start()
    {
        effects = GetComponent<Volume>();

        half.OnAtcivated += () =>
        {
            if (!isAnimating)
                StartCoroutine(ActivatedEffect());
        };
    }

    private IEnumerator ActivatedEffect()
    {
        isAnimating = true;
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            effects.weight = Mathf.Lerp(1,0,timer/duration);
            yield return null;
        }

        isAnimating = false;
    }
}