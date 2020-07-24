using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closedChestShot : MonoBehaviour
{
    public bool dropChest = false;
    public GameObject objExplosion;
    public GameObject objOpenedChest;

    public GameObject armor25;
    public GameObject armor50;
    public GameObject armor100;
    public GameObject healPotion;
    public GameObject speedPotion;
    public GameObject damagePotion;
    public GameObject winningCoin;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(objExplosion, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
            spawnItem();
            Destroy(Instantiate(objOpenedChest, new Vector3(4.19f, -2f, -5f), Quaternion.identity), 1f);
            Destroy(explosion, 1f);
        }
    }

    void spawnItem()
    {
        float random = Random.Range(0f, 100f);

        switch (GlobalVar.instance.lvl)
        {
            case 1:
                print("");
                break;
            case 2:
                print("");
                break;
            default:
                print("");
                break;
        }
        HealthSystem healthSystem = HealthSystem.instance;
        if(GlobalVar.instance.lvl == 6)
        {
            Instantiate(winningCoin, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (healthSystem.getCurrentHealth() != healthSystem.getHealtMaxAmount() && healthSystem.getCurrentArmor() != healthSystem.getArmorMaxAmount())
        {
            spawnAll(random);
        }
        else if (healthSystem.getCurrentHealth() == healthSystem.getHealtMaxAmount() && healthSystem.getCurrentArmor() != healthSystem.getArmorMaxAmount())
        {
            spawnNoHealthItem(random);
        }
        else if (healthSystem.getCurrentHealth() == healthSystem.getHealtMaxAmount() && healthSystem.getCurrentArmor() == healthSystem.getArmorMaxAmount())
        {
            spawnNoHealthAndArmor(random);
        }

    }

    private void FixedUpdate()
    {
        if (false)
        {

        }
    }

    private void spawnAll(float random)
    {
        if (random >= 0f && random <= 10f)
        {

            Instantiate(speedPotion, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 10f && random <= 30f)
        {
            Instantiate(damagePotion, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 30f && random <= 50f)
        {
            Instantiate(healPotion, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 50f && random <= 80f)
        {
            Instantiate(armor25, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 80f && random <= 95f)
        {
            Instantiate(armor50, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 95f && random <= 100f)
        {
            Instantiate(armor100, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
    }

    private void spawnNoHealthItem(float random)
    {
        if (random >= 0f && random <= 10f)
        {

            Instantiate(speedPotion, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 10f && random <= 30f)
        {
            Instantiate(damagePotion, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 30f && random <= 50f)
        {
            if (random < 40f)
                Instantiate(damagePotion, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
            else
                Instantiate(armor50, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 50f && random <= 80f)
        {
            Instantiate(armor25, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 80f && random <= 95f)
        {
            Instantiate(armor50, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else if (random > 95f && random <= 100f)
        {
            Instantiate(armor100, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
    }

    private void spawnNoHealthAndArmor(float random)
    {
        Debug.Log(random);
        if (random >= 0f && random <= 50f)
        {
            Instantiate(speedPotion, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
        }
        else
            Instantiate(damagePotion, new Vector3(4.19f, -2f, -5f), Quaternion.identity);
    }
}
