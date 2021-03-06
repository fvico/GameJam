﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    [SerializeField]
    LayerMask _layerPlayer;
    [Header("Solo activar si es la luz del menu")]
    [SerializeField]
    bool _isLightMenu;
    private void Awake()
    {
        if(_player == null)
        {
            if (_isLightMenu)
            {

            }
            else
            {
                _player = FindObjectOfType<DetectLight>().gameObject;
            }
        }
    }

    void Update()
    {
        transform.LookAt(_player.transform);
        Vector3 destino = transform.TransformDirection (Vector3.forward);
        float maxDistance = Vector3.Distance(transform.position, _player.transform.position);
        RaycastHit hit;

        if (DetectLight._inRangeLight)
        {
            if (Physics.Raycast(transform.position, destino, out hit, maxDistance,_layerPlayer))
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    DetectLight._inLight = true;
                    Debug.DrawRay(transform.position, destino, Color.green);
                }
                else
                {
                    DetectLight._inLight = false;
                    Debug.DrawRay(transform.position, destino, Color.red);
                }    
            }
        }
    }

   
}
