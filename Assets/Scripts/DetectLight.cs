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
    Material _materialInLight;
    [SerializeField]
    Material _materialInShadow;
    [SerializeField]
    GameObject _sytemParticles;
    [SerializeField]
    float _speedToDisolver;
    [SerializeField]
    float _minDisolver = -0.7f;
    [SerializeField]
    float _maxDisolver =  1.15f;
    float _countToDisolver = -0.7f;
    [SerializeField]
    Renderer _renderTrail;
    [SerializeField]
    Renderer _renderDark;
    [SerializeField]
    Renderer _renderShadow;
    [SerializeField]
    Animator _globalVolumen;
    [SerializeField]
    GameObject _rejillas;



    private void Start()
    {
        _renderer.material = _materialInLight;
        MovePlayer._speed = 2;
        _inRangeLight = false;
        _inLight = false;
        
    }

    private void Update()
    {   
        
        if (_inLight)
        {
            _globalVolumen.SetBool("PPIsActive", false);
            _countToDisolver += Time.deltaTime *_speedToDisolver;
            if(_countToDisolver > _maxDisolver)
            {
                _renderer.material = _materialInLight;
                _countToDisolver = _maxDisolver;
            }
            _renderTrail.material.SetFloat("_DisolverVFX", _countToDisolver);
            _renderDark.material.SetFloat("_DisolverVFX", _countToDisolver);
            _renderShadow.material.SetFloat("_DisolverVFX", _countToDisolver);
            _renderer.material.SetFloat("_Disolver",_countToDisolver);
            _sytemParticles.SetActive(false);
        }
        else
        {
            _globalVolumen.SetBool("PPIsActive", true);
            _renderer.material = _materialInShadow;
            _countToDisolver -= Time.deltaTime *_speedToDisolver;
            if(_countToDisolver < _minDisolver)
            {
                _countToDisolver = _minDisolver;
            }
            _renderTrail.material.SetFloat("_DisolverVFX", _countToDisolver);
            _renderDark.material.SetFloat("_DisolverVFX", _countToDisolver);
            _renderShadow.material.SetFloat("_DisolverVFX", _countToDisolver);
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
