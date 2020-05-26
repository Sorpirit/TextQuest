using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greow : MonoBehaviour
{
    [SerializeField] private LeanTweenType appear;
    [SerializeField] private float appDelay;
    [SerializeField] private float appDuratuion;

    [SerializeField] private LeanTweenType onClik;
    [SerializeField] private float clickDuratin;
    
    [SerializeField] private LeanTweenType onHover;
    [SerializeField] private float hoverDuration;

    public Action OnClick;

    private void OnEnable()
    {
        AppearAnimation();
    }

    private void AppearAnimation()
    {
        transform.localScale = new Vector3(.3f,.3f,.3f);
        LeanTween.scale(gameObject, Vector3.one, appDuratuion).setDelay(appDelay).setEase(appear);
        LeanTween.alpha(gameObject, 0, 0).setEase(appear);
        LeanTween.alpha(gameObject, 1f, appDuratuion).setDelay(appDelay).setEase(appear);
    }

    public void Click()
    {
        LeanTween.scale(gameObject, new Vector3(1.1f,1.1f,1.1f), clickDuratin).setEase(onClik).setOnComplete(OnClick);
    }

    public void Hover()
    {
        LeanTween.scale(gameObject, new Vector3(1.05f, 1.05f, 1), hoverDuration).setEase(onHover);
    }

    public void ExitHover()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), hoverDuration).setEase(onHover);
    }
}
