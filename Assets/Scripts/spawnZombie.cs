using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class spawnZombie : MonoBehaviour
{

    
    Vector3[] spawnLocations = new [] { new Vector3(-7.8f,1.27f,0), 
        new Vector3(-10.51f, -3.22f,0),
        new Vector3(-8.33f,-8.73f,0),
        new Vector3(-0.04f,-6.17f,0),
        new Vector3(3.82f,-8.45f,0),
        new Vector3(9.15f,-8.41f,0),
        new Vector3(17.25f,-8.53f,0),
        new Vector3(20.5f,-6.29f,0),
        new Vector3(20.62f,-0.11f,0),
        new Vector3(14.89f,2.25f,0),
        new Vector3(10.88f,2.41f,0),
        new Vector3(6.59f,2.45f,0),
        new Vector3(0.38f,2.33f,0) };

    Vector3[] bossSpawnLocations = new[]
    {
        new Vector3(-10.5f, 2.5f, 0),
        new Vector3(20.5f, 2.5f, 0),
        new Vector3(-10.5f, -8.5f, 0),
        new Vector3(20.5f, -8.5f, 0)
    };
    public GameObject zombie;
    public GameObject boss;
    private bool bossSpawned = false;


    public TMPro.TextMeshProUGUI killsText;
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI damageText;
    public TMPro.TextMeshProUGUI waveText;
    public TMPro.TextMeshProUGUI staticKillText;

    private Transform player;
    GameObject bossObject;

    // Start is called before the first frame update
    void Start()
    {
        if(zombie == null)
        {
            return;
        }

        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        updateStats();
        StartCoroutine(spawner());

        //spawnBoss();

    }

    void updateStats()
    {
        killsText.text = "" + GlobalVar.instance.kill_count + "/" + GlobalVar.instance.getKills();
        healthText.text = "" + GlobalVar.instance.getHealth();
        damageText.text = "" + GlobalVar.instance.getDamage();
        waveText.text = "" + (GlobalVar.instance.lvl + 1) + "/" + GlobalVar.instance.MAXLEVEL;

    }
    void spawn()
    {
        
        if(false){ //debug
            for(int i = 0; i < spawnLocations.Length; i++){
                GameObject newZombie = Instantiate(zombie, spawnLocations[i], Quaternion.identity);
                
                moveToPlayer zombie_data = newZombie.GetComponent<moveToPlayer>();
                zombieStats zombie_stats = newZombie.GetComponent<zombieStats>();
                zombie_data.zombieSpeed = GlobalVar.instance.getSpawnSpeed();
                zombie_stats.health = GlobalVar.instance.getHealth();
                zombie_stats.damage = GlobalVar.instance.getDamage();
                zombie_data.zombieSpeed = 0.1f;
            }
        }
        else if(!GlobalVar.instance.bossMode){
            int randomNum = (int)Random.Range(0f, spawnLocations.Length - 0.01f);
            float randomSpeed = Random.Range(1f, 3f);
            GameObject newZombie = Instantiate(zombie, spawnLocations[randomNum], Quaternion.identity);
            Debug.Log("Called");
            newZombie.GetComponent<AIDestinationSetter>().target = player;
            

            AIPath zombie_data = newZombie.GetComponent<AIPath>();
            zombie_data.maxSpeed = randomSpeed;
            

            zombieStats zombie_stats = newZombie.GetComponent<zombieStats>();
            zombie_stats.health = GlobalVar.instance.getHealth();
            zombie_stats.damage = GlobalVar.instance.getDamage();
        }
        else
        {
            if(!bossSpawned)
            {
                spawnBoss();
            }
            int randomNum = (int)Random.Range(0f, bossSpawnLocations.Length - 0.01f);
            GameObject newZombie = Instantiate(zombie, bossSpawnLocations[randomNum], Quaternion.identity);


            newZombie.GetComponent<AIDestinationSetter>().target = player;


            AIPath zombie_data = newZombie.GetComponent<AIPath>();
            zombie_data.maxSpeed = 8f;


            zombieStats zombie_stats = newZombie.GetComponent<zombieStats>();
            zombie_stats.health = 1;
            zombie_stats.damage = 15;
        }
    }

    void spawnBoss()
    {
        bossSpawned = true;
        bossObject = Instantiate(boss, new Vector2(0, 0), Quaternion.identity);
        bossObject.GetComponent<AIDestinationSetter>().target = player;
        GlobalVar.instance.playerDamage = 1;
        AIPath boss_data = boss.GetComponent<AIPath>();
        boss_data.maxSpeed = 2.5f;
        zombieStats boss_stats = bossObject.GetComponent<zombieStats>();
        boss_stats.health = 150;
        boss_stats.damage = 30;
        GlobalVar.instance.bossSpawned = true;
        staticKillText.text = "Boss:";
        GlobalVar.instance.updateStats = true;
    }

    IEnumerator spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(GlobalVar.instance.spawnTime);
            if (GlobalVar.instance.spawning)
            {
                if (!GlobalVar.instance.bossMode) {
                    if (GlobalVar.instance.zombie_count < GlobalVar.instance.getKills())
                    {
                        if (!GlobalVar.instance.paused)
                        {
                            spawn();
                            GlobalVar.instance.zombie_count++;
                            updateStats();
                        }
                    }
                }
                else
                {
                    if (!GlobalVar.instance.paused)
                    {
                        spawn();
                    }
                }
            }
            if(GlobalVar.instance.updateStats){
                GlobalVar.instance.updateStats = false;
                updateStats();
            }
            
        }
    }


    private void FixedUpdate()
    {
        if (GlobalVar.instance.lvl < GlobalVar.instance.MAXLEVEL)
        {
            if (GlobalVar.instance.kill_count == GlobalVar.instance.getKills() && GlobalVar.instance.spawning && !GlobalVar.instance.bossMode)
            {
                GlobalVar.instance.spawning = false;
                lvlUp();
            }
            else if(GlobalVar.instance.bossSpawned && !bossObject)
            {
                GlobalVar.instance.bossSpawned = false;
                GlobalVar.instance.spawning = false;
                destroyZombies();
                lvlUp();
            }
        }
    }

    void lvlUp()
    {
        lvlUpSpawnClosedChest.spawn = true;
    }


    void spawnRandom()
    {
        while (Random.Range(0.0f, 100.0f) > 50.0f)
        {
            Vector3 randPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z);
            GameObject spawnedZombie = Instantiate(zombie, randPos, Quaternion.identity);
        }
        return;
    }

    void destroyZombies()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("zombie");
        for (int i = 0; i < zombies.Length; i++)
        {
            Destroy(zombies[i]);
        }
    }
}
