using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtraparPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    [SerializeField]
    Animator _FadeInOut;
    [SerializeField]
    AudioClip audioMuerte;


    private void Awake()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<MovePlayer>().gameObject;
        }
    }

    IEnumerator ReinicioNivel()
    {
        MovePlayer._canPlayer = false;
        MovePlayer.emisor.mute = false;
        MovePlayer.emisor.loop = false;
        MovePlayer.emisor.PlayOneShot(audioMuerte);

        _FadeInOut.SetBool("IsFadeIn", true);
        
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.sceneCount); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            StartCoroutine(ReinicioNivel());

        }
    }
}
