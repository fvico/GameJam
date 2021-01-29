using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    PauseMenu _pauseMenu;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _pauseMenu.Win();
        }
    }
}
