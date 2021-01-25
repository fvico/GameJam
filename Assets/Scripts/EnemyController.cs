using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent _navMeshAgent;
    [SerializeField]
    Transform _targetPlayer;
    Vector3 _targetOrigen;
    [SerializeField]
    Transform _targetDestino;
    bool _canPlayerDetect;
    [SerializeField]
    Renderer _renderer;
    private void Start()
    {
        _renderer.material.SetColor("_Color", new Color(1f, 1f, 1f));
        _canPlayerDetect = false;
        _targetOrigen = transform.position;
       
    }

    private void Update()
    {
        
        if (DetectLight._inLight)
        {
            if (_canPlayerDetect)
            {
                ChangeDestino(_targetPlayer.position);
            }
        }
        else
        {
            print("Entro");
            ChangeDestino(_targetOrigen);
        }
        
    }

    void ChangeDestino(Vector3 destino)
    {
        _navMeshAgent.destination = destino;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _canPlayerDetect = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _canPlayerDetect = false;
        }
    }
}
