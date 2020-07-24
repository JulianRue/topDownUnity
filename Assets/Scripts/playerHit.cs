using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHit : MonoBehaviour
{
    public GameObject objExplosion;
    public GameObject objBlood1;
    public GameObject objBlood2;
    public GameObject objBlood3;
    public AudioSource sound;
    public AudioClip soundclip;

    private bool dealDamage = true;

    public void Start()
    {
        if (sound == null)
        {
            return;
        }

        sound.clip = soundclip;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if ((collision.gameObject.name.Contains("zombie") ||collision.gameObject.name.Contains("boss")) && dealDamage)
        {
            dealDamage = false;
            StartCoroutine("damageDealer");
            HealthSystem.instance.Damage(collision.gameObject.GetComponent<zombieStats>().damage);

            if (HealthSystem.instance.getCurrentHealth() <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(3);
            }
            
            // Calculate Angle Between the collision point and the player
            //Vector3 dir = collision.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            //dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir*9000);

            /*
            Destroy(collision.gameObject);
            GlobalVar.instance.kill_count++;
            
            GlobalVar.instance.updateStats = true;
            */
            
            spawnBlood(1.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name.Contains("car"))
        {
            HealthSystem.instance.Damage(collision.gameObject.GetComponent<carStats>().damage);
            if (HealthSystem.instance.getCurrentHealth() <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(3);
            }
            spawnBlood(4f);
        }
    }

    void spawnBlood(float time){
            float random = Random.Range(0f, 3f);
            GameObject blood;
            if (random < 1f)
            {
                blood = Instantiate(objBlood1, gameObject.transform.position, Quaternion.identity);
            }
            else if( random < 2f)
            {
                blood = Instantiate(objBlood2, gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                blood = Instantiate(objBlood3, gameObject.transform.position, Quaternion.identity);
            }


            //Destroy(explosion, 0.3f);

            sound.Play();
            Destroy(blood, time);
        }

    IEnumerator damageDealer()
    {
        yield return new WaitForSeconds(0.5f);
        dealDamage = true;
    }
}
