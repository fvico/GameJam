using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheelLights : MonoBehaviour
{
    [SerializeField]
    float _speedRotate;
    [SerializeField]
    Vector3 _directionRotate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_directionRotate * _speedRotate);
    }
}
