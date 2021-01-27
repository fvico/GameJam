using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraparPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    [SerializeField]
    Transform _positionInicialPlayer;
    [SerializeField]
    Animator _FadeInOut;

    IEnumerator ReinicioNivel()
    {
        _FadeInOut.SetBool("IsFadeIn", true);
        yield return new WaitForSeconds(1.9f);
        _player.transform.position = _positionInicialPlayer.position;
        _FadeInOut.SetBool("IsFadeIn", false);

        // yield return new WaitForSeconds(30f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ReiniciarNivel")
        {
            print("Cchoque");
        }
    }
}
