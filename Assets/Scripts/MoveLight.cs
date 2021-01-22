using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLight : MonoBehaviour
{
    [SerializeField]
    bool _isMoving;
    float _speed;
    Vector3 _origen;
    [SerializeField]
    Transform _destinationLight;
    [SerializeField]
    float _timeToMove;

    [SerializeField]
    AnimationCurve _curveTime;
    private void Start()
    {
        if (_isMoving)
        {
            _origen = transform.position;
            _destinationLight.position = new Vector3(_destinationLight.position.x, transform.position.y, _destinationLight.position.z);
            StartCoroutine(Move(_origen, _destinationLight.position));
        }
        
    }
    private void Update()
    {
        if(_isMoving)
        {
            _speed = 1;
        }
        else
        {
            _speed = 0;
        }

    }

    IEnumerator Move(Vector3 start, Vector3 destination)
    {
        for(float i = 0; i<_timeToMove; i+= Time.deltaTime * _speed)
        {
            yield return 0f;
            transform.position = Vector3.Lerp(start, destination, _curveTime.Evaluate( i / _timeToMove));
        }
        transform.position = destination;
        StartCoroutine(Move(destination, start));
    }
}
