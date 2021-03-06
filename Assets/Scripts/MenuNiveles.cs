﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNiveles : MonoBehaviour
{
    public static bool _fadeIn = false;
    public static int _nivelesSuperados = 0;

    private AudioSource FXAudioSource;

    [SerializeField]
    Animator _FadeInOut;
    [SerializeField]
    GameObject[] niveles;

    void Start()
    {
        Time.timeScale = 1f;
        FXAudioSource = GetComponent<AudioSource>();
        FXAudioSource.mute = PauseMenu._FXMuted;
        FXAudioSource.volume = PauseMenu._volumenFX;
        _nivelesSuperados = PlayerPrefs.GetInt("NivelesSuperados", _nivelesSuperados);
        ActualizarInterfaz();
        print(_nivelesSuperados);
    }

    public void Menu()
    {
        _FadeInOut.SetTrigger("IsFadeIn");
        StartCoroutine(WaitMenu());
    }

    public void Level1()
    {
        _FadeInOut.SetTrigger("IsFadeIn");
        MovePlayer._actualLevel = 1;
        StartCoroutine(WaitLevel1());

    }

    public void Level2()
    {
        _FadeInOut.SetTrigger("IsFadeIn");
        MovePlayer._actualLevel = 2;
        StartCoroutine(WaitLevel2());

    }

    public void Level3()
    {
        _FadeInOut.SetTrigger("IsFadeIn");
        MovePlayer._actualLevel = 3;
        StartCoroutine(WaitLevel3());

    }

    public void Level4()
    {
        _FadeInOut.SetTrigger("IsFadeIn");
        MovePlayer._actualLevel = 4;
        StartCoroutine(WaitLevel4());

    }

    public void Level5()
    {
        _FadeInOut.SetTrigger("IsFadeIn");
        MovePlayer._actualLevel = 5;
        StartCoroutine(WaitLevel5());

    }



    IEnumerator WaitMenu()
    {
        yield return new WaitForSeconds(3f);
        //yield return new WaitUntil(() => FXAudioSource.isPlaying == false);

        SceneManager.LoadScene("Menu");

    }

    IEnumerator WaitLevel1()
    {
        yield return new WaitForSeconds(3f);
        //yield return new WaitUntil(() => FXAudioSource.isPlaying == false);

        PlayerPrefs.SetInt("LevelToLoad", 1);
        SceneManager.LoadScene("Charlie");
    }

    IEnumerator WaitLevel2()
    {
        yield return new WaitForSeconds(3f);
        //yield return new WaitUntil(() => FXAudioSource.isPlaying == false);

        PlayerPrefs.SetInt("LevelToLoad", 2);
        SceneManager.LoadScene("Charlie");
    }
    IEnumerator WaitLevel3()
    {

        yield return new WaitForSeconds(3f);
        //yield return new WaitUntil(() => FXAudioSource.isPlaying == false);

        PlayerPrefs.SetInt("LevelToLoad", 3);
        SceneManager.LoadScene("Charlie");
    }
    IEnumerator WaitLevel4()
    {

        yield return new WaitForSeconds(3f);
        //yield return new WaitUntil(() => FXAudioSource.isPlaying == false);

        PlayerPrefs.SetInt("LevelToLoad", 4);
        SceneManager.LoadScene("Charlie");
    }
    IEnumerator WaitLevel5()
    {

        yield return new WaitForSeconds(3f);
        //yield return new WaitUntil(() => FXAudioSource.isPlaying == false);

        PlayerPrefs.SetInt("LevelToLoad", 5);
        SceneManager.LoadScene("Charlie");
    }



    public void ActualizarInterfaz()
    {
        foreach(GameObject g in niveles) {
            g.SetActive(false);
        }

        for(int i = 0; i< _nivelesSuperados+1; i++)
        {
            niveles[i].SetActive(true);
        }
        PlayerPrefs.SetInt("NivelesSuperados", _nivelesSuperados);
    }
}
