using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterEnemyScript : MonoBehaviour
{
    [SerializeField]
    private float xspeed;
    [SerializeField]
    private float yspeed;
    [SerializeField]
    private int health;
    private Transform _transform;
    private Transform _playerTransform;
    private Rigidbody2D _RB;
    private GameController _gameController;
    private GameObject _player;
    private Vector2 direction;
    [SerializeField]
    private GameObject meleePrefab;
    [SerializeField]
    private GameObject rangedPrefab;
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private float attackTime;

    private enum attackType
    {
        Melee,
        Ranged
    }
    private attackType attack = attackType.Melee;


    // Start is called before the first frame update
    void Start()
    {
        //cache components
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        _player = GameObject.Find("Player");
        _transform = GetComponent<Transform>();
        _playerTransform = _player.GetComponent<Transform>();
        _RB = GetComponent<Rigidbody2D>();
        direction = transform.TransformDirection(Vector2.up);

        InvokeRepeating("meleeAttack", attackTime, attackRate);
    }

    // Update is called once per frame
    void Update()
    {
        //Only update if the current game state is Playing
        if (_gameController.GetState() != GameController.EGameState.Playing)
            return;
        move();



    }

    void move()
    {
        float translationx = xspeed;
        float translationy = yspeed;

        //get the distance between the enemy and the player
        float xdist = _transform.position.x - _playerTransform.position.x;
        float ydist = _transform.position.y - _playerTransform.position.y;

        //move left if to the right of the player
        if (xdist >= 0)
        {
            translationx = -translationx;
        }
        //move down if above the player
        if (ydist >= 0)
        {
            translationy = -translationy;
        }

        // Make it move x meters per second instead of x meters per frame
        translationx *= Time.deltaTime;
        translationy *= Time.deltaTime;
        Vector2 moveposition = _transform.position + new Vector3(translationx, translationy);
        _RB.MovePosition(moveposition);
    }

    void meleeAttack()
    {
        //get the distance between the enemy and the player
        float xdist = _transform.position.x - _playerTransform.position.x;
        float ydist = _transform.position.y - _playerTransform.position.y;

        xdist = Mathf.Abs(xdist);
        ydist = Mathf.Abs(ydist);

        Quaternion rotation = Quaternion.Euler(0, 0, getDirection()[0]);
        float startPosX = getDirection()[1];
        float startPosY = getDirection()[2];
        GameObject createdAttack = Instantiate(meleePrefab, _transform.position + new Vector3(startPosX, startPosY, 0), rotation);
        Destroy(createdAttack, 0.2f);
    }

    void rangedAttack()
    {

    }

    void takeDamage()
    {
        health -= 1;
        if (health == 0)
        {
            onDeath();
        }
    }

    void onDeath()
    {
        _gameController.score += 50;
        Destroy(gameObject);
    }

    private int[] getDirection()
    {
        //get the distance between the enemy and the player
        float xdist = _transform.position.x - _playerTransform.position.x;
        float ydist = _transform.position.y - _playerTransform.position.y;
        int startPosX = 0;
        int startPosY = 0;
        int _direction = 0;


        if ((Mathf.Abs(xdist) <= 1) || (Mathf.Abs(ydist) <= 1))
        {
            if (((ydist >= 0) && (ydist > xdist)))
            {
                //UP
                _direction = 0;
                startPosY = 1;
                startPosX = 0;
                //  _direction = transform.TransformDirection(Vector2.up);
            }

            if (((xdist >= 0) && (xdist > ydist)))
            {
                //LEFT
                _direction = 90;
                startPosX = -1;
                //startPosY = 0;
                // _direction = transform.TransformDirection(Vector2.left);
            }

            if (((ydist >= 0) && (ydist > xdist)))
            {
                //DOWN
                _direction = 180;
                startPosY = -1;
                startPosX = 0;
                // _direction = transform.TransformDirection(Vector2.down);
            }

            if (((xdist <= 0) && (xdist < ydist)))
            {
                //RIGHT
                _direction = 270;
                startPosX = 1;
                startPosY = 0;
                // _direction = transform.TransformDirection(Vector2.right);
            }
        }
        Debug.Log(xdist);
        Debug.Log(ydist);



        int[] returns = { _direction, startPosX, startPosY };

        return returns;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    }
}
