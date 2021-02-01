using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static float _volumenMusica = 0.4f;
    public static float _volumenFX = 0.2f;
    public static bool _musicaMuted = false;
    public static bool _FXMuted = false;

    [SerializeField]
    GameObject[] elementosUI;
    [SerializeField]
    GameObject muteButtonMusic;
    [SerializeField]
    GameObject muteButtonFX;
    [SerializeField]
    Sprite iconoMuted;
    [SerializeField]
    Sprite iconoUnmuted;
    [SerializeField]
    bool noPlayer;
    [SerializeField]
    Animator _FadeInOut;

    private AudioSource musicAudioSource;
    private AudioSource FXAudioSource;
    private AudioSource FXPlayerAudioSource;
    private List<AudioSource> audioSourcesExtras = new List<AudioSource>();
    private Slider musicSlider;
    private Slider FXSlider;
    private bool goingMenu = false;
    private bool goingLevels = false;

    void Start()
    {
        InicializarPanel();
    }

    void Update()
    {
        AjustarValores();
    }

    public void InicializarPanel()
    {
        musicAudioSource = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
        if (!noPlayer)
        {
            FXPlayerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Interruptor");

        foreach (GameObject g in temp)
        {
            audioSourcesExtras.Add(g.GetComponent<AudioSource>());
        }

        temp = GameObject.FindGameObjectsWithTag("Win");

        foreach (GameObject g in temp)
        {
            audioSourcesExtras.Add(g.GetComponent<AudioSource>());
        }

        temp = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject g in temp)
        {
            audioSourcesExtras.Add(g.GetComponent<AudioSource>());
        }

        EstadoPorDefecto();
        FXAudioSource = GetComponent<AudioSource>();
        musicSlider = elementosUI[5].GetComponentInChildren<Slider>();
        FXSlider = elementosUI[6].GetComponentInChildren<Slider>();
    }
        


    public void AjustarValores()
    {
        musicSlider.value = _volumenMusica;
        FXSlider.value = _volumenFX;

        if (_FXMuted)
        {
            if (!noPlayer)
            {
                FXPlayerAudioSource.mute = true;
            }
            FXAudioSource.mute = true;

            foreach (AudioSource aSource in audioSourcesExtras)
            {
                aSource.mute = true;
            }


            muteButtonFX.gameObject.GetComponent<Image>().sprite = iconoMuted;
        }
        else
        {
            if (!noPlayer)
            {
                FXPlayerAudioSource.mute = false;
                FXPlayerAudioSource.volume = _volumenFX + 0.4f;
            }
            FXAudioSource.mute = false;
            FXAudioSource.volume = _volumenFX;

            foreach (AudioSource aSource in audioSourcesExtras)
            {
                aSource.mute = false;
                aSource.volume = _volumenFX;
            }

            muteButtonFX.gameObject.GetComponent<Image>().sprite = iconoUnmuted;
        }

        if (_musicaMuted)
        {
            musicAudioSource.mute = true;
            muteButtonMusic.gameObject.GetComponent<Image>().sprite = iconoMuted;
        }
        else
        {
            musicAudioSource.mute = false;
            musicAudioSource.volume = _volumenMusica;
            muteButtonMusic.gameObject.GetComponent<Image>().sprite = iconoUnmuted;
        }
    }

    public void SetVolumenMusica()
    {
        _volumenMusica = musicSlider.value;
    }

    public void SetVolumenFX()
    {
        _volumenFX = FXSlider.value;
    }


    public void Continue()
    {
        StartCoroutine(WaitContinue());
    }

    public void SelectLevel()
    {
        goingLevels = true;
        if (SceneManager.GetActiveScene().name == "Testing" || SceneManager.GetActiveScene().name == "Charlie")
        {
            if (!MovePlayer._win)
            {
                elementosUI[0].SetActive(false);
                elementosUI[1].SetActive(false);
                elementosUI[2].SetActive(false);
                elementosUI[3].SetActive(false);
                elementosUI[4].SetActive(false);
                elementosUI[5].SetActive(false);
                elementosUI[6].SetActive(false);
                elementosUI[7].SetActive(false);
                elementosUI[8].SetActive(false);
                elementosUI[9].SetActive(false);
                elementosUI[10].SetActive(false);
                elementosUI[11].SetActive(false);
                elementosUI[12].SetActive(false);
                elementosUI[13].SetActive(false);
                elementosUI[14].SetActive(true);
                elementosUI[15].SetActive(true);
                elementosUI[16].SetActive(true);
                elementosUI[17].SetActive(false);
            }
            else
            {
                _FadeInOut.SetTrigger("IsFadeIn");
                StartCoroutine(WaitSelectLevel());
            }
        }

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            _FadeInOut.SetTrigger("IsFadeIn");
            StartCoroutine(WaitSelectLevel());
        }

            
    }

    public void Options()
    {
        StartCoroutine(WaitOptions());
    }

    public void Menu()
    {
        goingMenu = true;
        StartCoroutine(WaitMenu());
    }
    public void Back()
    {
        StartCoroutine(WaitBack());
    }

    public void FullScreen()
    {
        StartCoroutine(WaitFullScreen());
    }

    public void MutearMusica()
    {
        StartCoroutine(WaitMutearMusica());
    }

    public void MutearFX()
    {
        StartCoroutine(WaitMutearFX());
    }

    public void Controls()
    {
        StartCoroutine(WaitControls());
    }

    public void Exit()
    {
        StartCoroutine(WaitExit());
    }

    public void Yes()
    {
        StartCoroutine(WaitYes());
    }

    public void No()
    {
        StartCoroutine(WaitNo());
    }

    IEnumerator WaitContinue()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        MovePlayer._paused = false;
    }

    IEnumerator WaitSelectLevel()
    {
        //yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        if (SceneManager.GetActiveScene().name == "Testing" || SceneManager.GetActiveScene().name == "Charlie")
        {
    

            goingLevels = false;
            MovePlayer._paused = false;
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Menu2");

        }
        
        if (SceneManager.GetActiveScene().name == "Menu")
        {

            SceneManager.LoadScene("Menu2");
            yield return new WaitForSeconds(3f);
            goingLevels = false;
        }

    }

    IEnumerator WaitOptions()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        elementosUI[0].SetActive(false);
        elementosUI[1].SetActive(false);
        elementosUI[2].SetActive(false);
        elementosUI[3].SetActive(false);
        elementosUI[4].SetActive(true);
        elementosUI[5].SetActive(true);
        elementosUI[6].SetActive(true);
        elementosUI[7].SetActive(true);
        elementosUI[8].SetActive(false);
        elementosUI[9].SetActive(false);
        elementosUI[10].SetActive(false);
        elementosUI[11].SetActive(false);
        elementosUI[12].SetActive(false);
        elementosUI[13].SetActive(false);
        elementosUI[14].SetActive(false);
        elementosUI[15].SetActive(false);
        elementosUI[16].SetActive(false);
        elementosUI[17].SetActive(false);
    }
    IEnumerator WaitMenu()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);    
        if (SceneManager.GetActiveScene().name == "Testing" || SceneManager.GetActiveScene().name == "Charlie" )
        {
            elementosUI[0].SetActive(false);
            elementosUI[1].SetActive(false);
            elementosUI[2].SetActive(false);
            elementosUI[3].SetActive(false);
            elementosUI[4].SetActive(false);
            elementosUI[5].SetActive(false);
            elementosUI[6].SetActive(false);
            elementosUI[7].SetActive(false);
            elementosUI[8].SetActive(false);
            elementosUI[9].SetActive(false);
            elementosUI[10].SetActive(false);
            elementosUI[11].SetActive(false);
            elementosUI[12].SetActive(false);
            elementosUI[13].SetActive(false);
            elementosUI[14].SetActive(true);
            elementosUI[15].SetActive(true);
            elementosUI[16].SetActive(true);
            elementosUI[17].SetActive(false);
        }
        else 
        {
            SceneManager.LoadScene("Menu");
            goingMenu = false;
        }
    }

    IEnumerator WaitBack()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        EstadoPorDefecto();
    }

    IEnumerator WaitFullScreen()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1920, 1080, false);
            elementosUI[4].GetComponentInChildren<Text>().text = "WINDOWED";
        }
        else
        {
            Screen.SetResolution(1920, 1080, true);
            elementosUI[4].GetComponentInChildren<Text>().text = "FULL  SCREEN";
        }
    }

    IEnumerator WaitMutearMusica()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        _musicaMuted = !_musicaMuted;
    }

    IEnumerator WaitMutearFX()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        _FXMuted = !_FXMuted;
    }

    IEnumerator WaitControls()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        elementosUI[0].SetActive(false);
        elementosUI[1].SetActive(false);
        elementosUI[2].SetActive(false);
        elementosUI[3].SetActive(false);
        elementosUI[4].SetActive(false);
        elementosUI[5].SetActive(false);
        elementosUI[6].SetActive(false);
        elementosUI[7].SetActive(true);
        elementosUI[8].SetActive(false);
        elementosUI[9].SetActive(false);
        elementosUI[10].SetActive(true);
        elementosUI[11].SetActive(true);
        elementosUI[12].SetActive(true);
        elementosUI[13].SetActive(true);
        elementosUI[14].SetActive(false);
        elementosUI[15].SetActive(false);
        elementosUI[16].SetActive(false);
        elementosUI[17].SetActive(false);
    }

    IEnumerator WaitExit()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        Application.Quit();
    }

    IEnumerator WaitYes()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        
        if (goingMenu)
        {
            print("Hola1");
            goingMenu = false;
            SceneManager.LoadScene("Menu");
        }
        if (goingLevels)
        {
            print("Hola");
            goingLevels = false;
            SceneManager.LoadScene("Menu2");
        }
    }

    IEnumerator WaitNo()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        EstadoPorDefecto();
    }

    private void EstadoPorDefecto()
    {
        if (SceneManager.GetActiveScene().name == "Testing" || SceneManager.GetActiveScene().name == "Charlie")
        {
            if (!MovePlayer._win)
            {
                elementosUI[0].SetActive(true);
                elementosUI[1].SetActive(true);
                elementosUI[2].SetActive(true);
                elementosUI[3].SetActive(true);
                elementosUI[4].SetActive(false);
                elementosUI[5].SetActive(false);
                elementosUI[6].SetActive(false);
                elementosUI[7].SetActive(false);
                elementosUI[8].SetActive(false);
                elementosUI[9].SetActive(false);
                elementosUI[10].SetActive(false);
                elementosUI[11].SetActive(false);
                elementosUI[12].SetActive(false);
                elementosUI[13].SetActive(false);
                elementosUI[14].SetActive(false);
                elementosUI[15].SetActive(false);
                elementosUI[16].SetActive(false);
                elementosUI[17].SetActive(false);
            }
            else
            {
                elementosUI[0].SetActive(false);
                elementosUI[1].SetActive(true);
                elementosUI[2].SetActive(false);
                elementosUI[3].SetActive(false);
                elementosUI[4].SetActive(false);
                elementosUI[5].SetActive(false);
                elementosUI[6].SetActive(false);
                elementosUI[7].SetActive(false);
                elementosUI[8].SetActive(false);
                elementosUI[9].SetActive(false);
                elementosUI[10].SetActive(false);
                elementosUI[11].SetActive(false);
                elementosUI[12].SetActive(false);
                elementosUI[13].SetActive(false);
                elementosUI[14].SetActive(false);
                elementosUI[15].SetActive(false);
                elementosUI[16].SetActive(false);
                elementosUI[17].SetActive(true);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            elementosUI[0].SetActive(false);
            elementosUI[1].SetActive(true);
            elementosUI[2].SetActive(true);
            elementosUI[3].SetActive(false);
            elementosUI[4].SetActive(false);
            elementosUI[5].SetActive(false);
            elementosUI[6].SetActive(false);
            elementosUI[7].SetActive(false);
            elementosUI[8].SetActive(true);
            elementosUI[9].SetActive(true);
            elementosUI[10].SetActive(false);
            elementosUI[11].SetActive(false);
            elementosUI[12].SetActive(false);
            elementosUI[13].SetActive(false);
            elementosUI[14].SetActive(false);
            elementosUI[15].SetActive(false);
            elementosUI[16].SetActive(false);
            elementosUI[17].SetActive(false);
        }
    }
    public void Win()
    {
        EstadoPorDefecto();
        MovePlayer._paused = true;
    }
}

