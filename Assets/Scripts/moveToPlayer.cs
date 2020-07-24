using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToPlayer : MonoBehaviour
{
    public float zombieSpeed = 1f;

    public Transform player;
    public Rigidbody2D body;

    public moveToPlayer(int a)
    {

    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    void FixedUpdate()
    {
        if (GlobalVar.instance.paused)
        {
            return;
        }

        if(player.position == null)
        {
            print("null");
            return;
        }
        //wenn zu nah, angreifen und nicht mehr bewegen
        if (Vector2.Distance(transform.position, player.position) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, zombieSpeed * Time.deltaTime);
            
            //Vector von aktueller zombie position zum player punkt
            Vector2 lookDir = player.position - transform.position;

            // atan -> https://prnt.sc/pgvqxd
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            body.rotation = angle;
        }
        
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        zombieSpeed /= 2f;
    }

    void OnTriggerExit2D(Collider2D collider) {
        zombieSpeed *= 2f;
    }
}
