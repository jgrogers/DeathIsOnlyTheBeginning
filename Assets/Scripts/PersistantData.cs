using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    public int day = 1;
    public int souls = 0;
    public float targetRadius = 50f;
    public float playerHealth = 100f;
    public float playerMaxHealth = 100f;
    public GameObject origin;
    public static PersistantData Instance {get; private set;}
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
