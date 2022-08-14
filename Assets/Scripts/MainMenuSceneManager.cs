using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEditor;


public class MainMenuSceneManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public GameObject MainMenu;
    public GameObject OptionsMenu;

    public void Start()
    {
    }

    public void LoadA()
    {
        Debug.Log("sceneName to load: " + "GameLoop");
        SceneManager.LoadScene("GameLoop");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void Options()
    {
        //MainMenu.SetActive(false);
    }

    public void Back()
    {
        //OptionsMenu.SetActive(false);
        //MainMenu.SetActive(true);
    }


}
