using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public AudioClip click;

    public GameObject volumeMeter;
    public GameObject Fading;
    public GameObject pig_canon;
    public Text namaCredits;

    AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        volumeMeter.SetActive(false);
        sound.volume = volumeMeter.GetComponent<Slider>().value;

    }

    public void PlayButton()
    {
        sound.PlayOneShot(click , .7f);
        Fading.GetComponent<Animator>().SetBool("fade",true);
        SceneManager.LoadScene("level01");
    }

    public void Setting()
    {
        sound.PlayOneShot(click , .7f);
        if(volumeMeter.activeInHierarchy)
        {
            volumeMeter.SetActive(false);
        }
        else
        {
            volumeMeter.SetActive(true);
        }
    }

    public void SettingVolume()
    {
        sound.volume = volumeMeter.GetComponent<Slider>().value;
    }

    public void Credits()
    {
        sound.PlayOneShot(click , .7f);

        Animator[] anim = pig_canon.GetComponentsInChildren<Animator>();
        foreach (Animator a in anim)
        {
            a.SetTrigger("shoot");
        }
        if(namaCredits.text == "Art = PixelFrog")
        {namaCredits.text = "Programmer = Ivannuari";}
        else{namaCredits.text = "Art = PixelFrog";}

    }
}
