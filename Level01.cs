using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Level01 : MonoBehaviour
{
    public AudioClip click;
    AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void ContinueButton()
    {
        sound.PlayOneShot(click,1f);
    }

    public void MenuButton()
    {
        sound.PlayOneShot(click,1f);
        SceneManager.LoadScene("menu");
    }

    public void ExitButton()
    {
        sound.PlayOneShot(click,1f);
        Application.Quit();
    }
}
