using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Niveles : MonoBehaviour
{


    [MenuItem("Niveles/Test")]
    public static void SetLevelToTest()
    {
        SetLevel(0);
    }

    [MenuItem("Niveles/1")]
    public static void SetLevelTo1()
    {
        SetLevel(1);
    }

    [MenuItem("Niveles/2")]
    public static void SetLevelTo2()
    {
        SetLevel(2);
    }

    [MenuItem("Niveles/3")]
    public static void SetLevelTo3()
    {
        SetLevel(3);
    }

    [MenuItem("Niveles/4")]
    public static void SetLevelTo4()
    {
        SetLevel(4);
    }

    [MenuItem("Niveles/5")]
    public static void SetLevelTo5()
    {
        SetLevel(5);
    }

    public static void SetLevel(int level)
    {
        PlayerPrefs.SetInt("ActualLevel", level);
        GameObject padreDeTodos = GameObject.FindGameObjectWithTag("PadreDeTodos");
        if(level < padreDeTodos.transform.childCount)
        {
            PlayerPrefs.SetInt("LevelToLoad", level);
            for(int i = 0; i < padreDeTodos.transform.childCount; i++)
            {
                padreDeTodos.transform.GetChild(i).gameObject.SetActive(false);
            }
            padreDeTodos.transform.GetChild(level).gameObject.SetActive(true);
        }
    }

}
