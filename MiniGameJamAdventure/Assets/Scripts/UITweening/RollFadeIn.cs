using System.Collections;
using System.Collections.Generic;
using TextAdventure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollFadeIn : MonoBehaviour
{
    [SerializeField] private LeanTweenType appear;
    [SerializeField] private float appDuratuion;
    [SerializeField] private Vector2 startingPoint;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private bool useOnlyAlpha;

    void OnEnable()
    {
        AppearAnimation();
    }

    private void AppearAnimation()
    {
        Vector2 targetPoint = transform.position;
        _canvasGroup.alpha = 0;
        float dealy = 0;
        AnswerUI answerUi = GetComponent<AnswerUI>();
        if (answerUi != null)
            dealy = answerUi.apearDelay;

        _canvasGroup.alpha = 0;
        if (!useOnlyAlpha)
        {
            transform.position += (Vector3) startingPoint;
            LeanTween.move(gameObject, targetPoint, appDuratuion).setEase(appear);
        }


        LeanTween.alphaCanvas(_canvasGroup, 1, appDuratuion).setDelay(dealy).setEase(appear);
    }

    public void FadeOutDestroy()
    {
        LeanTween.alphaCanvas(_canvasGroup, 0, appDuratuion).setEase(appear).setOnComplete(() => Destroy(gameObject));
    }
}