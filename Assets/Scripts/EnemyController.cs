﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    [SerializeField]
    Transform _targetPlayer;
    private void Start()
    {
        GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _navMeshAgent.destination = _targetPlayer.position;
    }
}
