﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _switch;
    [SerializeField]
    GameObject _meshOn;
    [SerializeField]
    GameObject _meshOff;
    [SerializeField]
    float _time;
    [SerializeField]
    bool _isTime;
    [SerializeField]
    bool _haveMoreSwitch;
    [SerializeField]
    [Header("_numSwitchToActivate Tiene que coincider el numero de interructores de este tipo")]
    float _numSwitchToActivate;
    public static float _currentNumSwitchToActivate;
    bool _isActive = true;


    private void Start()
    {
        _meshOff.SetActive(false);
        _meshOn.SetActive(true);
        _currentNumSwitchToActivate = 0;
    }
    
    IEnumerator BotonForTime()
    {
       
        for (int i = 0; i < _switch.Count; i++)
        {
            _switch[i].SetActive(false);
            _meshOff.SetActive(true);
            _meshOn.SetActive(false);
        }
        yield return new WaitForSeconds(_time);
        for (int i = 0; i < _switch.Count; i++)
        {
            _switch[i].SetActive(true);
            _meshOff.SetActive(false);
            _meshOn.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (_isTime)
            {
                StartCoroutine(BotonForTime());
            }
            else
            {
                for (int i = 0; i < _switch.Count; i++)
                {
                    if (!_haveMoreSwitch)
                    {
                        _switch[i].SetActive(false);
                    }
                    _meshOff.SetActive(true);
                    _meshOn.SetActive(false);
                }
            }

            if (_haveMoreSwitch)
            {
                
                if (_isActive)
                {
                    _isActive = false;
                    _currentNumSwitchToActivate++;
                }
                if(_currentNumSwitchToActivate >= _numSwitchToActivate)
                {
                    _currentNumSwitchToActivate = _numSwitchToActivate;

                    for (int i = 0; i < _switch.Count; i++)
                    {
                        _switch[i].SetActive(false);
                    }

                }
            }
            
        }
    }
}
