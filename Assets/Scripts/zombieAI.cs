using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class zombieAI : MonoBehaviour
{

    public float speed = 400f;
    public float damage = 1;
    public float health = 1;

    private Transform player;
    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;
    private bool pathDone = false;
    private int currentWaypoint = 0;

    public const float NEXT_WAYPOINT_DISTANCE = 3f;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        InvokeRepeating("updatePath", 0f, 0.5f);
        
    }

    void updatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, player.position, OnPathComplete);
    }

    private void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        else
        {
            //
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            pathDone = true;
            return;
        }
        else
        {
            pathDone = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        Vector2 lookDir = direction - new Vector2(transform.position.x, transform.position.y);

        // atan -> https://prnt.sc/pgvqxd
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < NEXT_WAYPOINT_DISTANCE)
        {
            currentWaypoint++;
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
