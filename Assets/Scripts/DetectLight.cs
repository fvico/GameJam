using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour
{

    public static bool _inRangeLight;
    public static bool _inLight;
    [SerializeField]
    Renderer _renderer;
    [SerializeField]
    GameObject _sytemParticles;
    [SerializeField]
    float _speedToDisolver;
    float _countToDisolver = -0.7f;


    private void Start()
    {
        MovePlayer._speed = 2;
        _inRangeLight = false;
        _inLight = false;
        
    }

    private void Update()
    {   
        
        if (_inLight)
        {
           
            _countToDisolver += Time.deltaTime *_speedToDisolver;
            if(_countToDisolver > 1)
            {
                _countToDisolver = 1f;
            }
            print(_countToDisolver);
            _renderer.material.SetFloat("_Disolver",_countToDisolver);
            _sytemParticles.SetActive(false);
        }
        else
        {
           
            _countToDisolver -= Time.deltaTime *_speedToDisolver;
            if(_countToDisolver < -0.7f)
            {
                _countToDisolver = -0.7f;
            }
            _renderer.material.SetFloat("_Disolver", _countToDisolver);
            _sytemParticles.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Light")
        {
            MovePlayer._speed = 2;
            _inRangeLight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Light")
        {
            MovePlayer._speed = 10;
            _inRangeLight = false;
            _inLight = false;
        }

    }
}
