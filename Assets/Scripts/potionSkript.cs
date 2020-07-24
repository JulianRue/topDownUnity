using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionSkript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
        {
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            if (gameObject.name.Contains("healPotion"))
                HealthSystem.instance.HealHealth(50);
            else if (gameObject.name.Contains("damagePotion"))
                GlobalVar.instance.playerDamage += 1;
            else if (gameObject.name.Contains("speedPotion")){
                //collision.gameObject.GetComponent<PlayerMovement>().moveSpeed += 1;
                GlobalVar.instance.playerMoveSpeed += 1f;
            }

            Destroy(gameObject);            
            GlobalVar.instance.levelUp();
        }
    }
}
