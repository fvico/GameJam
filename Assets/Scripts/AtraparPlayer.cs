using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtraparPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    [SerializeField]
    Transform _positionInicialPlayer;
    [SerializeField]
    Animator _FadeInOut;
    //[SerializeField]
    

    private void Start()
    {
       
    }

    IEnumerator ReinicioNivel()
    {
        _FadeInOut.SetBool("IsFadeIn", true);
        
        yield return new WaitForSeconds(1.9f);

        SceneManager.LoadScene("Testing"); 

        // yield return new WaitForSeconds(30f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Cchoque");
            StartCoroutine(ReinicioNivel());
            //_canPlayer = false;
        }
    }
}
