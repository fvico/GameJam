using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static float _volumenMusica = 1f;
    public static float _volumenFX = 0.6f;
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

    private AudioSource musicAudioSource;
    private AudioSource FXAudioSource;
    private AudioSource FXPlayerAudioSource;
    private Slider musicSlider;
    private Slider FXSlider;


    void Start()
    {
        musicAudioSource = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
        if (!noPlayer)
        {
            FXPlayerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
        FXAudioSource = GetComponent<AudioSource>();
        musicSlider = elementosUI[5].GetComponentInChildren<Slider>();
        FXSlider = elementosUI[6].GetComponentInChildren<Slider>();
    }

    void Update()
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
            muteButtonFX.gameObject.GetComponent<Image>().sprite = iconoMuted;
        }
        else
        {
            if (!noPlayer)
            {
                FXPlayerAudioSource.mute = false;
                FXPlayerAudioSource.volume = _volumenFX;
            }
            FXAudioSource.mute = false;
            FXAudioSource.volume = _volumenFX;
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
        StartCoroutine(WaitSelectLevel());
    }

    public void Options()
    {
        StartCoroutine(WaitOptions());
    }

    public void Menu()
    {
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

    IEnumerator WaitContinue()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        MovePlayer._paused = false;
    }

    IEnumerator WaitSelectLevel()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        ////////////////////////////////
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
    }
    IEnumerator WaitMenu()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        /////////////////////////////////
    }

    IEnumerator WaitBack()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        elementosUI[0].SetActive(true);
        elementosUI[1].SetActive(true);
        elementosUI[2].SetActive(true);
        elementosUI[3].SetActive(true);
        elementosUI[4].SetActive(false);
        elementosUI[5].SetActive(false);
        elementosUI[6].SetActive(false);
        elementosUI[7].SetActive(false);
    }

    IEnumerator WaitFullScreen()
    {
        yield return new WaitUntil(() => FXAudioSource.isPlaying == false);
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1920, 1080, true);
            elementosUI[4].GetComponentInChildren<Text>().text = "WINDOWED";
        }
        else
        {
            Screen.SetResolution(1920, 1080, false);
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
}
