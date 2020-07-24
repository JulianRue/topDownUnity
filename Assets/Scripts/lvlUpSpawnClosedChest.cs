using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlUpSpawnClosedChest : MonoBehaviour
{
    public static bool spawn = false;
    public AudioSource sound;
    public AudioClip soundclip;
    public GameObject chest;

    void Start()
    {
        if (sound == null)
        {
            return;
        }

        sound.clip = soundclip;
    }

    void FixedUpdate()
    {
        if (spawn)
        {
            spawn = false;
            sound.Play();
            Instantiate(chest, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
    }
}
