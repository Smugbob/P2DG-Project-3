using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    [SerializeField]
    private int health;
    private Transform _transform;
    private Rigidbody2D _RB;
    private GameController _gameController;
    [SerializeField]
    private GameObject planks;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        _transform = GetComponent<Transform>();
        _RB = GetComponent<Rigidbody2D>(); //cached components
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        health -= damage; //health is deducted when called
        if (health <= 0)
        {
            onDeath(); //call death function when no more health remaining
        }
    }

    void onDeath()
    {
        FindObjectOfType<AudioManager>().Play("explode");
        GameObject createdPlanks = Instantiate(planks, transform.position, transform.rotation);
        Destroy(gameObject); //remove the object from the environment
    }
}
