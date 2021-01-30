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



    private void Awake()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<MovePlayer>().gameObject;
        }
        print(SceneManager.sceneCount);
    }

    IEnumerator ReinicioNivel()
    {
        _FadeInOut.SetBool("IsFadeIn", true);
        
        yield return new WaitForSeconds(1.9f);

        SceneManager.LoadScene(SceneManager.sceneCount); 

        // yield return new WaitForSeconds(30f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            StartCoroutine(ReinicioNivel());
            MovePlayer._canPlayer = false;
        }
    }
}
