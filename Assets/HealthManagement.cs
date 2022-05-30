using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManagement : MonoBehaviour
{
    public void TakeDamage(float damage)
    {
       PersistantData.Instance.playerHealth -= damage;
       if (PersistantData.Instance.playerHealth < 0.0f)
            PersistantData.Instance.playerHealth = 0.0f;
    }
    public void TakeHealing(float healing) {
       PersistantData.Instance.playerHealth += healing;
       if (PersistantData.Instance.playerHealth > PersistantData.Instance.playerMaxHealth)
            PersistantData.Instance.playerHealth = PersistantData.Instance.playerMaxHealth;
    }

}
