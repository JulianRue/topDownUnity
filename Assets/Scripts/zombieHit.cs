using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            gameObject.GetComponent<zombieStats>().setDamage(GlobalVar.instance.playerDamage);
            Destroy(collision.gameObject);
            if(GlobalVar.instance.bossMode && gameObject.name.Contains("boss")) {
                GlobalVar.instance.kill_count++;
                GlobalVar.instance.updateStats = true;
            }
        }
        
    }
}
