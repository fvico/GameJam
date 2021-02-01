using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    
    [SerializeField]
    int tiempo;
    [SerializeField]
    Animator _FadeInOut;
    [SerializeField]
    Text timerText;
    [SerializeField]
    AudioClip audioMuerte;

    private int s;


    public void StartTimer()
    {
        s = tiempo;
        WriteTimer(s);
        Invoke("UpdateTimer", 1f);
    }

    public void StopTimer()
    {
        CancelInvoke();
    }

    public void UpdateTimer()
    {
        s--;
        WriteTimer(s);
        if (s == 0)
        {
            _FadeInOut.SetTrigger("IsFadeIn");
            StartCoroutine(ReinicioNivel());
            return;
        }
        Invoke("UpdateTimer", 1f);
    }

    public void WriteTimer(int seg)
    {
        if(s < 10)
        {
            timerText.text = "0" + s.ToString();
        }
        else
        {
            timerText.text = s.ToString();
        }
    }

    IEnumerator ReinicioNivel()
    {
        StopTimer();

        if (!MovePlayer.reseteando)
        {
            MovePlayer.reseteando = true;
            MovePlayer._canPlayer = false;
            MovePlayer.emisor.mute = false;
            MovePlayer.emisor.loop = false;
            MovePlayer.emisor.PlayOneShot(audioMuerte);



            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene(SceneManager.sceneCount);
        }
    }
}
