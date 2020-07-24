using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class zombieStats : MonoBehaviour
{

    public GameObject explosion;
    public GameObject hitmarker;

    public int health = 1;
    public int damage = 1;

    private bool working = true;
    private AIPath zombie_data;
    private float speedBackup = 1;

    private void Start()
    {
        zombie_data = gameObject.GetComponent<AIPath>();
    }

    private void Update()
    {
        if(GlobalVar.instance.paused && working)
        {
            working = false;
            gameObject.GetComponent<Renderer>().enabled = false ;
            speedBackup = zombie_data.maxSpeed;
            zombie_data.maxSpeed = 0;
        }
        else if(!GlobalVar.instance.paused && !working)
        {
            working = true;
            gameObject.SetActive(true);
            gameObject.GetComponent<Renderer>().enabled = true;
            zombie_data.maxSpeed = speedBackup;
        }
    }

    public void setDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            GameObject boom = Instantiate(explosion, transform.position, Quaternion.identity);

            // 16 frame animation bei 48 frames die sekunde
            Destroy(boom, 0.3f);

            //Destroy zombie
            Destroy(gameObject);

            //respawn random neue zombies
            //spawnRandom();
            if(!GlobalVar.instance.bossMode) {
                GlobalVar.instance.kill_count++;
                GlobalVar.instance.updateStats = true;
            }
                
        }
        else
        {
            //hitmarker
            //GameObject boom = Instantiate(hitmarker, transform.position, Quaternion.identity);
            GameObject boom = Instantiate(hitmarker, gameObject.transform.position, Quaternion.identity);
            // 14 frame animation bei 64 frames die sekunde
            Destroy(boom, 0.25f);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        zombie_data.maxSpeed /= 2f;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        zombie_data.maxSpeed *= 2f;
    }
}
