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
    Vector3 _targetActual;
    [SerializeField]
    Transform _targetDestino;
    bool _canPlayerDetect;
    [SerializeField]
    Renderer _renderer;
    [SerializeField]
    bool luzPerseguidora;
    [SerializeField]
    bool enemyPatrol;

    private void Awake()
    {
        if(_targetPlayer == null)
        {
            _targetPlayer = FindObjectOfType<MovePlayer>().transform;
        }
    }

    private void Start()
    {
        _renderer.material.SetColor("_Color", new Color(1f, 1f, 1f));
        _canPlayerDetect = false;
        _targetOrigen = transform.position;
        _targetActual = _targetDestino.position;

    }

    private void Update()
    {
        /*
                if(transform.position == _targetOrigen)
                {
                    ChangeDestino(_targetDestino.position);
                    print("u");
                }
                else if(transform.position == _targetDestino.position)
                {
                    ChangeDestino(_targetOrigen);
                    print("A");
                }
                */

        if (!luzPerseguidora)
        {
            if (!enemyPatrol)
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
                    ChangeDestino(_targetActual);
                }
            }
            else
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
                    /*if (transform.position.x == _targetOrigen.x && transform.position.z == _targetOrigen.z)
                    {
                        _targetActual = _targetDestino.position;
                        print("ENTRO " + transform.position);
                        print("STATE " + _navMeshAgent.isStopped);
                    }
                    else if (transform.position.x == _targetDestino.position.x && transform.position.z == _targetDestino.position.z)
                    {
                        _targetActual = _targetOrigen;
                        //ChangeDestino(_targetOrigen);
                        print("hola");
                        
                    }*/

                    ChangeDestino(_targetActual);
                }
            }
        }
        else
        {
            ChangeDestino(_targetPlayer.position);
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
        if (other.tag == "DestinationEnemy")
        {
            _targetActual = _targetOrigen;
            ChangeDestino(_targetOrigen);
        }

        if (other.tag == "OrigenEnemy")
        {
            _targetActual = _targetDestino.position;
            ChangeDestino(_targetDestino.position);
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
