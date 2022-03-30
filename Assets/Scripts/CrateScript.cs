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

    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        _transform = GetComponent<Transform>();
        _RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            onDeath();
        }
    }

    void onDeath()
    {
        FindObjectOfType<AudioManager>().Play("explode");
        Destroy(gameObject);
    }
}
