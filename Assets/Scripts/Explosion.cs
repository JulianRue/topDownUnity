using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 2;
    public GameObject explosion;

    private void Start()
    {
        //explosion.transform.localScale.Set(2, 2, 0);
    }
    void FixedUpdate()
    {
        Grenade ent = gameObject.GetComponent<Grenade>();
        if (ent.speed > 0 || ent.spawned)
        {
            return;
        }

        ent.spawned = true;

        GameObject expl = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(expl, 0.5f);

        Collider2D[] hitList = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(Collider2D entity in hitList)
        {
            if (entity.name.Contains("zombie") || entity.name.Contains("boss"))
            {
                entity.GetComponent<zombieStats>().setDamage(GlobalVar.instance.getBombDamage());
            }
            else if (entity.name.Contains("player"))
            {
                HealthSystem.instance.Damage(gameObject.GetComponent<Grenade>().damage * 5);
            }
        }
    }
}
