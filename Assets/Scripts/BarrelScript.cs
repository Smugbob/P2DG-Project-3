using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [SerializeField]
    private int health;
    private Transform _transform;
    private Rigidbody2D _RB;
    private GameController _gameController;
    [SerializeField]
    private GameObject acidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
        {
            GameObject createdAcid = Instantiate(acidPrefab, transform.position, transform.rotation);
            onDeath();
        }
    }

    void onDeath()
    {
        Destroy(gameObject);
    }
}
