using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private float KProjectileMoveSpeed = 10.0f;
    int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        //_transform.Rotate(Vector3.right, 90.f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translateAmount = Vector3.up * (Time.deltaTime * KProjectileMoveSpeed);

        _transform.Translate(translateAmount);
    }

    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if ((other.CompareTag("Enemy") || other.CompareTag("Alpha")) && gameObject.CompareTag("Laser"))
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<LesserEnemyScript>().takeDamage();
            }
            else
            {
                other.GetComponent<GreaterEnemyScript>().takeDamage();
            }
            Destroy(gameObject);
        }

        if ((other.CompareTag("Crate") || other.CompareTag("Barrel")))
        {
            if (other.CompareTag("Crate"))
            {
                other.GetComponent<CrateScript>().takeDamage(damage);
            }
            else
            {
                other.GetComponent<BarrelScript>().takeDamage(damage);
            }
            Destroy(gameObject);
        }

    }

}
