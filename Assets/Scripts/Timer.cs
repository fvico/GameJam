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

        _FadeInOut.SetBool("IsFadeIn", true);

        yield return new WaitForSeconds(1.9f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
