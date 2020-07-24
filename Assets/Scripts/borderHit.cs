using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borderHit : MonoBehaviour
{
    public GameObject explosion;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet_"))
        {
            GameObject explosionAnimation = Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity);

            // 16 frame animation bei 48 frames die sekunde
            Destroy(explosionAnimation, 0.3f);

            //destroy bullet
            Destroy(collision.gameObject);
        }
    }
}
