using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("zombie"))
        {
            GameObject explosionAnimation = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);

            // 16 frame animation bei 48 frames die sekunde
            Destroy(explosionAnimation, 0.3f);

            //destroy bullet
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name.Contains("car"))
        {
            GameObject explosionAnimation = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);

            // 16 frame animation bei 48 frames die sekunde
            Destroy(explosionAnimation, 0.3f);

            //destroy bullet
            Destroy(gameObject);
        }
    }
}
