/////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////// Referenced from Bernard Polidario on weeklyhow.com - 28th May 2021 /////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Healthbar : MonoBehaviour
{
    public Slider healthBar;
    public Health_System player_health;

    void Start()
    {
        player_health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_System>();
        healthBar = GetComponent<Slider>();
        //sets the maxvalue and value of the current health to be updated on every game start
        healthBar.maxValue = player_health.total_health;
        healthBar.value = player_health.total_health;

        SceneManager.SetActiveScene(gameObject.scene);
    }

    public void set_health(int hp)
    {
        //sets the value in the silder to an int passed to it
        healthBar.value = hp;
    }

}
