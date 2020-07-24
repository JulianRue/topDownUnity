using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVar : MonoBehaviour
{
    public static GlobalVar instance = null;

    public bool spawning = true;
    public bool bossMode = false;
    public bool bossSpawned = false;

    public float spawnTime = 0.7f;
    public int zombie_count = 0;
    public int kill_count = 0;
    public int lvl = 0;
    public int MAXLEVEL = 7;
    public int playerDamage = 1;
    public bool paused = false;
    public int maxBombs = 10;
    public int currentBombs = 0;
    public int difficulty = 0; // 0 = leicht, 1 = mittel, 2 = schwer
    public float playerMoveSpeed = 4f;
    public bool darkMode = false;
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject bossMap;

    public GameObject pictureWave1;
    public GameObject pictureWave2;
    public GameObject pictureWave3;
    public GameObject pictureWave4;
    public GameObject pictureWave5;
    public GameObject pictureWave6;
   


    private GameObject map2_spawned;
    private GameObject map1_spawned;
    private GameObject map3_spawned;
    //private int[] level_kills = { 50, 50, 50, 40, 50, 60, 1000 };
    private int[] level_kills = { 5, 5, 5, 5, 5, 5, 150 };
    private float[] level_spawn_time = { 0.2f, 0.0f, -0.2f, 0.1f, 0.0f, 0.0f, 0.00f };
    private int[] level_health = { 1, 2, 2, 1, 2, 2, 0 };
    private float[] level_zombie_speed = { 1.2f, 1.4f, 1.8f, 1.8f, 2f, 2f, 2f };
    private int[] level_zombie_damage = { 5, 10, 20, 20, 20, 20, 20 };
    private int[] bomb_damage = {1,1,2,2,2,2,3};
    
    // Start is called before the first frame update
        
    public bool updateStats = false;
    
    public void setDifficulty(){
        if(difficulty == 0){
            //bombs = 10
        }
        else if(difficulty == 1){
            //bombs = 7
        }
        else{
            //bombs = 5
        }
    }
    void Start()
    {
        GameObject wavePicture = Instantiate(pictureWave1, new Vector3(5.55f, -3.03f, 0f), Quaternion.identity);
        StartCoroutine(FadeOut(wavePicture));
        map1_spawned = Instantiate(map1, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public int getKills()
    {
        return level_kills[lvl];
    }

    public float getTime()
    {
        return level_spawn_time[lvl];
    }

    public int getHealth()
    {
        return level_health[lvl];
    }

    public float getSpawnSpeed()
    {
        return level_zombie_speed[lvl];
    }

    public int getDamage()
    {
        return level_zombie_damage[lvl];
    }

    public int getBombDamage(){
        return bomb_damage[lvl];
    }
    public void levelUp(){
        spawnTime -= GlobalVar.instance.getTime();
        lvl++;
        zombie_count = 0;
        kill_count = 0;

        if(lvl == 1){
            Destroy(map1_spawned);
            GameObject wavePicture = Instantiate(pictureWave2, new Vector3(5.55f, -3.03f, 0f), Quaternion.identity);
            StartCoroutine(FadeOut(wavePicture));
            map2_spawned = Instantiate(map2, new Vector3(0f, 0f, 0f), Quaternion.identity);
            
        }
        else if(lvl == 2){
            Destroy(map2_spawned);
            GameObject wavePicture = Instantiate(pictureWave3, new Vector3(5.55f, -3.03f, 0f), Quaternion.identity);
            StartCoroutine(FadeOut(wavePicture));
            map3_spawned = Instantiate(map3, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        else if (lvl == 3)
        {
            darkMode = true;
            Destroy(map3_spawned);
            GameObject wavePicture = Instantiate(pictureWave4, new Vector3(5.55f, -3.03f, 0f), Quaternion.identity);
            StartCoroutine(FadeOut(wavePicture));
            map1_spawned = Instantiate(map1, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        else if (lvl == 4)
        {
            Destroy(map1_spawned);
            GameObject wavePicture = Instantiate(pictureWave5, new Vector3(5.55f, -3.03f, 0f), Quaternion.identity);
            StartCoroutine(FadeOut(wavePicture));
            map2_spawned = Instantiate(map2, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        else if (lvl == 5)
        {
            Destroy(map2_spawned);
            GameObject wavePicture = Instantiate(pictureWave6, new Vector3(5.55f, -3.03f, 0f), Quaternion.identity);
            StartCoroutine(FadeOut(wavePicture));
            map3_spawned = Instantiate(map3, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        else if (lvl == 6){
            darkMode = false;
            Destroy(map3_spawned);
            GameObject wavePicture = Instantiate(pictureWave4, new Vector3(5.55f, -3.03f, 0f), Quaternion.identity);
            StartCoroutine(FadeOut(wavePicture));
            bossMode = true;
            map2_spawned = Instantiate(bossMap, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        else if (lvl == 7)
        {
            showWinningScreen();
        }
    }

    public void showWinningScreen()
    {
        SceneManager.LoadScene(4);
    }

    public int getCarDamage(){
        //todo variable setzen
        return 15;
    }

    IEnumerator FadeOut(GameObject gameObject)
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        for(float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
        AstarPath.active.Scan();
        spawning = true;
    }

    public void spawnZwischenMap()
    {
        GameObject zwischenMap  = Instantiate(bossMap, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Destroy(zwischenMap);
        
    }
}
