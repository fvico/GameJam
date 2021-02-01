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

    AudioSource emisor;
    bool alertaEmitida = false;

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
        emisor = GetComponent<AudioSource>();

    }

    private void Update()
    {


        if (!luzPerseguidora)
        {
            if (!enemyPatrol)
            {

                if (DetectLight._inLight)
                {
                    if (_canPlayerDetect)
                    {
                        if (!alertaEmitida)
                        {
                            emisor.Play();
                            alertaEmitida = true;
                        }
                        
                        ChangeDestino(_targetPlayer.position);
                    }
                }
                else
                {
                    alertaEmitida = false;
                    ChangeDestino(_targetActual);
                }
            }
            else
            {
                if (DetectLight._inLight)
                {
                    if (_canPlayerDetect)
                    {
                        if (!alertaEmitida)
                        {
                            emisor.Play();
                            alertaEmitida = true;
                        }
                        ChangeDestino(_targetPlayer.position);
                    }
                }
                else
                {
                    alertaEmitida = false;
                    ChangeDestino(_targetActual);
                }
            }
        }
        else
        {
            if (_canPlayerDetect)
            {
                if (!alertaEmitida)
                {
                    emisor.Play();
                    alertaEmitida = true;
                }
                ChangeDestino(_targetPlayer.position);
            }
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
