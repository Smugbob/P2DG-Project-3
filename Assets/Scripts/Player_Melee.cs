using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Melee : MonoBehaviour
{
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

        _transform.Translate(-translateAmount);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            Destroy(GameObject.Find("Enemy"));
            Destroy(GameObject.Find("Alpha Enemy"));
            Destroy(gameObject);
        }
    }
}
