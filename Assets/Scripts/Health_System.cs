////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////// This code was implemented following a tutorial from Bernard Polidario - 28th May 2021, on weeklyhow.com //////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_System : MonoBehaviour
{
    public int total_health = 100;
    public int current_health = 0;

    public Healthbar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        current_health = total_health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("damage taken");
            take_damage(10);
            if (current_health == 0)
            {
                Debug.Log("You have died");
            }

        }
        
    }

    public void take_damage(int damage)
    {
        current_health -= damage;
        healthBar.set_health(current_health);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("damage taken");
            take_damage(10);
            if (current_health == 0)
            {
                Debug.Log("You have died");
            }
        }
    }
}
///test comment ignore