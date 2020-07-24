using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onChestHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Contains("Bullet")){
            Destroy(collision.gameObject);
        }
    }
}
