using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotSeeker : MonoBehaviour,IDirectable
{

    [SerializeField] private bool isLookingForTarget;
    [SerializeField] private string targetTag;
    [SerializeField] private float lookDistance;
    [SerializeField] private float loseDistance;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float maxFromHomeDistance;
    [SerializeField] private Transform homePoint;
    
    public bool IsLookingForPlayer
    {
        get => isLookingForTarget;
        set => isLookingForTarget = value;
    }
    public Transform Target
    {
        get => _target;
        set
        {
            _isFoundTarget = value;
            _target = value;
        }
    }
    public Vector2 Direction
    {
        get => _targetDir;
    }

    public Transform FoundTarget
    {
        get => _target;
    }

    private float _accuracy = .45f;
    private bool _isFoundTarget;
    private Transform _target;
    private Vector2 _lastTargetPos;
    private Vector2 _targetDir;

    private void Awake()
    {
        _lastTargetPos = homePoint.position;
    }

    private void Update()
    {
        if (_isFoundTarget)
        {
            if (_target == null)
            {
                _isFoundTarget = false;
                CalculateDir();
            }
            else
            {
                _lastTargetPos = _target.position;
                CalculateDir();   
            }

            if (Vector2.Distance(transform.position, _lastTargetPos) > loseDistance)
            {
                _isFoundTarget = false;
                _target = null;
            }
                
        }
        else
        {
            if (Vector2.Distance(_lastTargetPos, transform.position) <= _accuracy)
            {
                if (Vector2.Distance(transform.position, homePoint.position) <= maxFromHomeDistance)
                {
                    TakeRandomPoint();
                }
                else
                {
                    _lastTargetPos = homePoint.position;
                    CalculateDir();
                }
            }
            else
            {
                CalculateDir();
            }
            
            if(isLookingForTarget)
                LookForTarget();
        }
        
        
    }

    private void TakeRandomPoint()
    {
        _lastTargetPos =(Vector2) transform.position + Random.insideUnitCircle.normalized * Random.Range(1,lookDistance);
        CalculateDir();
    }

    private void LookForTarget()
    {
        Collider2D[] posibleTargets = Physics2D.OverlapCircleAll(transform.position,lookDistance,targetMask);
        
        foreach (var posibleTarget in posibleTargets)
        {
            if (posibleTarget.CompareTag(targetTag))
            {
                _isFoundTarget = true;
                _target = posibleTarget.transform;
                _lastTargetPos = _target.position;
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Color l = new Color(.2f,.4f,.9f,.3f);
        Gizmos.color = l;
        Gizmos.DrawLine(transform.position,_lastTargetPos);
        Color r = new Color(.9f,.4f,.1f,.3f);
        Gizmos.color = r;
        Gizmos.DrawRay(transform.position,_targetDir);
        Color bgL = new Color(.2f,.6f,0,.1f);
        Gizmos.color = bgL;
        Gizmos.DrawSphere(transform.position,lookDistance);
        Color bgLO = new Color(.2f,.6f,.6f,.1f);
        Gizmos.color = bgLO;
        Gizmos.DrawSphere(transform.position,loseDistance);
    }

    private void CalculateDir()
    {
        _targetDir = Vector2.ClampMagnitude(_lastTargetPos - (Vector2) transform.position, 1);
    }
}
