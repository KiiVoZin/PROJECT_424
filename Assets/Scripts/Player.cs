using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealt;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealt = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TakeDamage(int damage)
    {
        currentHealt -= damage;
        healthBar.setHealth(currentHealt);
        if (currentHealt <= 0)
        {
            currentHealt = 0;
            Debug.Log("Dead");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
            TakeDamage(10);
            Debug.Log("Hit");
        }
    }
}
