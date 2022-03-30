﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    public int damage = 10;
    private Transform _transform;
    [SerializeField] private float KSlashMoveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translateAmount = Vector3.up * (Time.deltaTime * KSlashMoveSpeed);

        _transform.Translate(translateAmount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (((other.CompareTag("Enemy") || other.CompareTag("Alpha")) && gameObject.CompareTag("Melee")))
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<LesserEnemyScript>().takeDamage();
            }
            else
            {
                other.GetComponent<GreaterEnemyScript>().takeDamage();
            }
            //Destroy(gameObject);
        }

        if ((other.CompareTag("Player") && gameObject.CompareTag("EMelee")))
        {
            
            other.GetComponent<Health_System>().take_damage(damage);
            //Destroy(gameObject);
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
            //Destroy(gameObject);
        }

    }
}
