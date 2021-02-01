using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [SerializeField]
    PauseMenu _pauseMenu;

    AudioSource emisor;

    private void Start()
    {
        emisor = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (MovePlayer._actualLevel >= MenuNiveles._nivelesSuperados)
            {
                MenuNiveles._nivelesSuperados++;
            }
            emisor.Play();
            MovePlayer._win = true;
            _pauseMenu.Win();
        }
    }
}
