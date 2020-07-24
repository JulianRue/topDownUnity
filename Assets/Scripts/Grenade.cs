using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Vector2 crosshair_pos;

    
    public float speed = 3.5f;
    public int damage = 2;
    public bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        crosshair_pos = GameObject.Find("crosshair").transform.position;
    }
    
    void FixedUpdate()
    {
        if(speed > 0)
        {
            speed -= Random.Range(0.05f, 0.15f);
            transform.position = Vector2.MoveTowards(transform.position, crosshair_pos, speed * Time.deltaTime);
        }
        else
        {
            speed = 0;
            Destroy(gameObject, 0.2f);
            //explode
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("zombie") /*|| collision.gameObject.name.Contains("player")*/)
        {
            //speed = 0;
        }
    }
}
