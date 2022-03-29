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
    private float attackRate;
    [SerializeField]
    private float attackTime;
    private float minDist = 3.0f;
    private int rand = 0;
    [SerializeField]
    private int damage = 2;
    private bool attacking = false;


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


        transform.GetChild(1).gameObject.SetActive(false);
        

        InvokeRepeating("useAttack", attackTime, attackRate);
    }

    // Update is called once per frame
    void Update()
    {
        //Only update if the current game state is Playing
        if (_gameController.GetState() != GameController.EGameState.Playing)
            return;
        move();

        /*
        if (attack == attackType.Melee)
        {
            if ((Vector3.Distance(_playerTransform.position, _transform.position) < minDist) && (attacking = false))
            {
                attacking = true;
                InvokeRepeating("meleeAttack", attackTime, attackRate);
            }

            if ((Vector3.Distance(_playerTransform.position, _transform.position) < minDist) && (attacking = true))
            {
                attacking = true;
            }

            if ((Vector3.Distance(_playerTransform.position, _transform.position) > minDist) && (attacking = true))
            {
                attacking = false;
                CancelInvoke("meleeAttack");
            }
        }

        if (attack == attackType.Ranged)
        {
            if ((Vector3.Distance(_playerTransform.position, _transform.position) < minDist) && (attacking = false))
            {
                attacking = true;
                InvokeRepeating("rangedAttack", attackTime, attackRate * 2);
            }

            if ((Vector3.Distance(_playerTransform.position, _transform.position) < minDist) && (attacking = true))
            {
                attacking = true;
            }

            if ((Vector3.Distance(_playerTransform.position, _transform.position) > minDist) && (attacking = true))
            {
                attacking = false;
                CancelInvoke("rangedAttack");
            }
        }
        */





    }

    void useAttack()
    {
        if ((Vector3.Distance(_playerTransform.position, _transform.position) < minDist))
        {
            attacking = true;

            meleeAttack();
            Invoke("changeBackModel", 1.0f);
        }

        else
        {
            attacking = false;
        }

    }

    void move()
    {
        float translationx = xspeed;
        float translationy = yspeed;

        //get the distance between the enemy and the player
        //float xdist = _transform.position.x - _playerTransform.position.x;
        //float ydist = _transform.position.y - _playerTransform.position.y;

        // Quaternion rotation = Quaternion.Euler(0, 0, getDirection()[0]);
        // _transform.rotation = rotation;

        /*
        //move left if to the right of the player
        if ((xdist >= -minDist) && (xdist <= minDist))
        {
            translationy = 0;
        }
        else if (xdist > 0)
        {
            translationx = -xspeed;
        }
        if ((ydist >= -minDist) && (ydist <= minDist))
        {
            translationx = 0;
        }
        //move down if above the player
        else if (ydist > 0)
        {
            translationy = -yspeed;
        }
        */
        // Vector3 targetPosFlattened = new Vector3(_playerTransform.position.x, _playerTransform.position.y, 0);
        //_transform.LookAt(targetPosFlattened);
        _transform.up = _playerTransform.position - _transform.position;
        float step = yspeed * Time.deltaTime;

        
        _transform.position = Vector2.MoveTowards(_transform.position, _playerTransform.position, step);
        


        //Vector3 translateAmount = Vector3.up * (Time.deltaTime * yspeed);

        //_transform.Translate(translateAmount);

        // Make it move x meters per second instead of x meters per frame
        /*
        translationx *= Time.deltaTime;
        translationy *= Time.deltaTime;
        Vector2 moveposition = _transform.position + new Vector3(translationx, translationy);
        _RB.MovePosition(moveposition);
        */
    }

    void meleeAttack()
    {
        GameObject createdAttack;
        //get the distance between the enemy and the player
        //float xdist = _transform.position.x - _playerTransform.position.x;
        // float ydist = _transform.position.y - _playerTransform.position.y;

        // xdist = Mathf.Abs(xdist);
        // ydist = Mathf.Abs(ydist);

        //Quaternion rotation = Quaternion.Euler(0, 0, getDirection()[0]);
        //float startPosX = getDirection()[1];
        // float startPosY = getDirection()[2];
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        createdAttack = Instantiate(meleePrefab, _transform.position, _transform.rotation);
        createdAttack.transform.position += createdAttack.transform.up;
        createdAttack.transform.localScale += new Vector3(0.5f, 0.5f, 0);
        Destroy(createdAttack, 0.2f);
    }


    public void takeDamage()
    {

        health -= _player.GetComponent<ShipControl1>().playerAttack;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        //Debug.Log(xdist);
        //Debug.Log(ydist);



        int[] returns = { _direction, startPosX, startPosY };

        return returns;
    }

    void changeBackModel()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    
}
