using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Healthbar : MonoBehaviour
{
    public Slider healthBar;
    public Health_System player_health;

    // Start is called before the first frame update
    void Start()
    {
        player_health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_System>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = player_health.total_health;
        healthBar.value = player_health.total_health;

        SceneManager.SetActiveScene(gameObject.scene);
    }

    public void set_health(int hp)
    {
        healthBar.value = hp;
    }

}
