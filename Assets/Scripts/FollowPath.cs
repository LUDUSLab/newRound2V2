using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FollowPath : MonoBehaviour
{

    public enum FollowType
    {
        MoveTowards,
        lerp
    }

    public FollowType Type = FollowType.MoveTowards;
    public PathDefinition path;
    public float speed = 1;
    public float MaxDistanceToGoal = .1f;

    public float WaitTime, CountWaitTime;

    public IEnumerator<Transform> _currentPoint;


    // Use this for initialization
    void Start()
    {
        if (path == null)
        {
            Debug.LogError("path nao encontrado");
            return;
        }
        CountWaitTime = WaitTime;
        _currentPoint = path.GetPathEnumerator();
        _currentPoint.MoveNext();
        if (_currentPoint.Current == null)
        {
            return;
        }
        transform.position = _currentPoint.Current.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentPoint.Current == null || _currentPoint == null)
        {
            return;
        }
        if (Type == FollowType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);
        }
        else if (Type == FollowType.lerp)
        {
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);
        }
        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            if ((CountWaitTime -= Time.deltaTime) < 0)
            {
                _currentPoint.MoveNext();
                CountWaitTime = WaitTime;
            }
        }
    }
}
