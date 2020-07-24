using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    private int healthAmount = 100;
    private int healthMaxAmount = 100;
    private int armorAmount = 0;
    private int armorMaxAmount = 100;

    public static HealthSystem instance = null; 

    void Awake() {
        if(instance == null) 
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void Damage(int amount)
    {
        if(armorAmount > amount)
        {
            armorAmount -= amount;
        }
        else 
        {
            if (armorAmount > 0)
            {
                amount -= armorAmount;
                armorAmount = 0;
            }
            healthAmount -= amount;
        }

        if(healthAmount < 0)
        {
            healthAmount = 0;
        }

        if (OnDamaged != null)
        {
            OnDamaged(this, EventArgs.Empty);
        }
    }

    public void HealArmor(int amount)
    {
        armorAmount += amount;
        if(armorAmount > armorMaxAmount)
        {
            armorAmount = armorMaxAmount;
        }

        if (OnHealed != null)
        {
            OnHealed(this, EventArgs.Empty);
        }
    }

    public void HealHealth(int amount)
    {
        healthAmount += amount;
        if (healthAmount > healthMaxAmount)
        {
            healthAmount = healthMaxAmount;
        }

        if(OnHealed != null)
        {
            OnHealed(this, EventArgs.Empty);
        }
    }

    public float HealthToBar()
    {
        return (float)healthAmount / healthMaxAmount;
    }

    public float ArmorToBar()
    {
        return (float)armorAmount / armorMaxAmount;
    }

    public int getCurrentHealth()
    {
        return healthAmount;
    }

    public int getCurrentArmor()
    {
        return armorAmount;
    }

    public int getHealtMaxAmount()
    {
        return healthMaxAmount;
    }

    public int getArmorMaxAmount()
    {
        return armorMaxAmount;
    }
}
