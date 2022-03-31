using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    private Health_System player_health;

    // Start is called before the first frame update
    void Start()
    {
        player_health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_System>();
        InvokeRepeating("returnSize", 0.1f, 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        float posToBe = player_health.healthOpposite * 1.6f;
        if (player_health.current_health > player_health.total_health)
        {
            transform.position = new Vector3((-170), transform.position.y, transform.position.z); ;
        }
        else
        {
            transform.position = new Vector3((180 - posToBe), transform.position.y, transform.position.z);
        }
        
        
    }

    void returnSize()
    {
        
        transform.localScale = new Vector3(1, 1, 1);
        Invoke("upSize", 0.2f);
    }

    void upSize()
    {
        transform.localScale = new Vector3(0.75f, 0.75f, 0);
    }
}


