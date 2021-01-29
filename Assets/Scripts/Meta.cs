using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [SerializeField]
    PauseMenu _pauseMenu;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _pauseMenu.Win();
        }
    }
}
