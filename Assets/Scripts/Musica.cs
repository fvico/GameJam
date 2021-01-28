using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour
{


    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (PauseMenu._musicaMuted)
        {
            GetComponent<AudioSource>().mute = true;
        }
        else
        {
            GetComponent<AudioSource>().mute = false;
            GetComponent<AudioSource>().volume = PauseMenu._volumenMusica;
        }
    }
}
