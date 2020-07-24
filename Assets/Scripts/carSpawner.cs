using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSpawner : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    // Start is called before the first frame update
     Vector3[] spawnLocations = new [] { new Vector3(-30f, -3.5f,0), new Vector3(30f, -1.4f,0)};
     Vector3[] spawnRotations = new [] { new Vector3(0, 0,-90), new Vector3(0, 0, 90)};
    void Start()
    {
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        while (GlobalVar.instance.kill_count < GlobalVar.instance.getKills())
        {
            yield return new WaitForSeconds(Random.Range(0.8f, 3f));
            if(GlobalVar.instance.spawning){
                spawnCar();
            }
                
            
        }

        destroyAll();
    }

    void spawnCar(){
        int randomNum = (int)Random.Range(0f, 1.9f);
        Vector3 pos = spawnLocations[randomNum];
        GameObject car = Instantiate(car1, pos, Quaternion.identity);
        car.GetComponent<carStats>().damage = GlobalVar.instance.getCarDamage();
        car.transform.Rotate(spawnRotations[randomNum]);
        Rigidbody2D carRb = car.GetComponent<Rigidbody2D>();
        // force auf die bullet setzen
        carRb.AddForce(car.transform.up * 10f, ForceMode2D.Impulse);
        //nach 5 sekunden sicherheitshalber destroyen
        Destroy(car, 10f);
    }

    void destroyAll(){
        GameObject[] cars = GameObject.FindGameObjectsWithTag("car");
        for(int i = 0; i < cars.Length; i++){
            Destroy(cars[i]);
        }
         
    }
}
