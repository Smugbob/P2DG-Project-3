using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    public int damage = 0;
    private Transform _transform;
    private Transform _playerTransform;
    [SerializeField] private float KSlashMoveSpeed = 1f;
    Vector3 translator;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
    //alpha does 20
    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Melee"))
        {
            Vector3 translateAmount = Vector3.up * (Time.deltaTime * KSlashMoveSpeed);
            translator += translateAmount;
            _transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y, _playerTransform.position.z);

            _transform.Translate(translator);
        }
        else
        {
            Vector3 translateAmount = Vector3.up * (Time.deltaTime * KSlashMoveSpeed);

            _transform.Translate(translateAmount);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (((other.CompareTag("Enemy") || other.CompareTag("Alpha")) && gameObject.CompareTag("Melee")))
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<LesserEnemyScript>().takeDamage(damage);
            }
            else
            {
                other.GetComponent<GreaterEnemyScript>().takeDamage(damage);
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
