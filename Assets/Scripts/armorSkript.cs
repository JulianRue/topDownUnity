using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armorSkript : MonoBehaviour
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
            Destroy(gameObject);
            if (gameObject.name.Contains("armor_25"))
                HealthSystem.instance.HealArmor(25);
            else if (gameObject.name.Contains("armor_50"))
                HealthSystem.instance.HealArmor(50);
            else if (gameObject.name.Contains("armor_100"))
                HealthSystem.instance.HealArmor(100);

            Destroy(gameObject);            
            GlobalVar.instance.levelUp();
        }
    }
}
