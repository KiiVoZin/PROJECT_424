using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealt;
    public HealthBar healthBar;
    public static bool gameIsPaused=false;
    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealt = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealt==0){
            LoadA();
        }
    }

    void TakeDamage(int damage)
    {
        currentHealt -= damage;
        healthBar.setHealth(currentHealt);
        if (currentHealt <= 0)
        {
            currentHealt = 0;
        }
    }

    void RestoreHealth(int health)
    {
        currentHealt += health;
        if (currentHealt > maxHealth)
        {
            currentHealt = maxHealth;
        }
        healthBar.setHealth(currentHealt);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
            TakeDamage(20);
        }
        if (other.tag == "EnemyBullet")
        {
            other.gameObject.SetActive(false);
            TakeDamage(10);
        }
        if (other.tag == "Health")
        {   
            Destroy(other.gameObject);
            RestoreHealth(30);
        }
        if(other.tag=="Chest"){
            if(!gameIsPaused){
                pauseMenuUI.SetActive(true);
                Time.timeScale=0f;
                gameIsPaused=true;
            }
        }
    }
    
    

    public void LoadA()
    {
        Debug.Log("sceneName to load: " + "MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}
