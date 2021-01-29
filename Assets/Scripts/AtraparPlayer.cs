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


    IEnumerator ReinicioNivel()
    {
        _FadeInOut.SetBool("IsFadeIn", true);
        
        yield return new WaitForSeconds(1.9f);

        SceneManager.LoadScene("Charlie"); 

        // yield return new WaitForSeconds(30f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Cchoque");
            StartCoroutine(ReinicioNivel());
            MovePlayer._canPlayer = false;
        }
    }
}
