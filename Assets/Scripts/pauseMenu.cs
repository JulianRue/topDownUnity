using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{

    public GameObject menu;
    private long last = 0;

    public void Start()
    {
        last = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    public void Update()
    {
        if (Input.GetKey("escape"))
        {
            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (now - last > 400)
            {
                if (GlobalVar.instance.paused)
                {
                    
                    resume();
                }
                else
                {
                    
                    pause();
                }

                last = now;
            }
            
        }   
    }

    public void pause()
    {
        Cursor.visible = true;
        menu.SetActive(true);
        GlobalVar.instance.paused = true;
    }

    public void resume()
    {
        Cursor.visible = false;
        menu.SetActive(false);
        GlobalVar.instance.paused = false;
    }

    public void QuitGame()
    {
        print("exit");
        Application.Quit();
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
