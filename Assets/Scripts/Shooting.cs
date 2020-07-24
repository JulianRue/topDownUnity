using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;
    public AudioSource sound;
    public AudioClip soundclip;

    public float bulletForce = 10f;
    public bool multiple = true;
    
    // Start is called before the first frame update

    void Start()
    {
        if (sound == null)
        {
            return;
        }

        sound.clip = soundclip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !GlobalVar.instance.paused)
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.E) && !GlobalVar.instance.paused)
        {
            bomb();
        }
    }

    void bomb()
    {
        if(GlobalVar.instance.currentBombs > 0){
            Instantiate(grenadePrefab, firePoint.position, Quaternion.identity);
            GlobalVar.instance.currentBombs --;
        }
        
    }

    void Shoot()
    {        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // force auf die bullet setzen
        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        //nach 5 sekunden sicherheitshalber destroyen
        Destroy(bullet, 5f);

        sound.Play();
    }
}
