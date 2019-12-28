using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class item_lives : MonoBehaviour
{
    public AudioClip Hpsound;
    AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            sound.PlayOneShot(Hpsound , 1f);
            player Player = col.gameObject.GetComponent<player>();
            Player.currHealth += 1;
            Destroy(gameObject , .5f);
        }
    }
}
