using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarFade : MonoBehaviour
{

    public Image healthImage;
    public Image armorImage;
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI armorText;
    public static int health = 100;
    public static int armor = 100;

    private void Awake()
    {

    }

    private void Start()
    {
        
        setHealth(HealthSystem.instance.HealthToBar());
        setArmor(HealthSystem.instance.ArmorToBar());

        HealthSystem.instance.OnDamaged += HealthSystem_OnDamaged;
        HealthSystem.instance.OnHealed += HealthSystem_OnHealed;

    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        setHealth(HealthSystem.instance.HealthToBar());
        setArmor(HealthSystem.instance.ArmorToBar());
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        setHealth(HealthSystem.instance.HealthToBar());
        setArmor(HealthSystem.instance.ArmorToBar());
    }

    private void setHealth(float var)
    {
        if(healthImage != null)
        {
            healthImage.fillAmount = var;
        }

        if(healthText != null)
        {
            healthText.text = "Health: " + HealthSystem.instance.getCurrentHealth();
        }
    }

    private void setArmor(float var)
    {
        if(armorImage != null)
        {
            armorImage.fillAmount = var;
        }

        if (healthText != null)
        {
            armorText.text = "Armor: " + HealthSystem.instance.getCurrentArmor();
        }

    }
}

