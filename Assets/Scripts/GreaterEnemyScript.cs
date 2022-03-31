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
    private Vector2 direction; //various attributes and components to be used
    [SerializeField]
    private GameObject meleePrefab; //object to be stored with melee attack prefab
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private float attackTime;
    private float minDist = 3.0f;
    private int rand = 0;
    [SerializeField]
    private int damage = 2;
    private bool attacking = false;
    private GameObject slainCount;
    [SerializeField]
    private GameObject potionPrefab;
    public GameObject effect;
    public GameObject effect_small;
    public GameObject bloodPrefab;


    // Start is called before the first frame update
    void Start()
    {
        //cache components
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        slainCount = GameObject.Find("KnightsSlain");
        _player = GameObject.Find("Player");
        _transform = GetComponent<Transform>();
        _playerTransform = _player.GetComponent<Transform>();
        _RB = GetComponent<Rigidbody2D>(); //cache components
        direction = transform.TransformDirection(Vector2.up); //initial direction is upwards


        transform.GetChild(1).gameObject.SetActive(false); //hide the attacking pose
        

        InvokeRepeating("useAttack", attackTime, attackRate); //call the useAttack function after attackTime amount of seconds, subsequently call it every attackRate amount of seconds
    }

    // Update is called once per frame
    void Update()
    {
        //Only update if the current game state is Playing
        if (_gameController.GetState() != GameController.EGameState.Playing) //only update if the game is playing
            return;
        move(); //move every frame
    }

    void useAttack()
    {
        if ((Vector3.Distance(_playerTransform.position, _transform.position) < minDist)) //if the player is within the range of minDist from the enemy
        {
            attacking = true; //the enemy is allowed to attack

            meleeAttack(); //use a melee attack
            Invoke("changeBackModel", 1.0f); //change the pose back to the neutral one after 1 second
        }

        else
        {
            attacking = false; //attacking is always false otherwise
        }

    }

    void move()
    {

        float translationy = yspeed;

        _transform.up = _playerTransform.position - _transform.position; //get the angle the enemy needs to be facing towards the player
        float step = yspeed * Time.deltaTime; //speed relative to time

        
        _transform.position = Vector2.MoveTowards(_transform.position, _playerTransform.position, step); //move in the direction of the player
        
    }

    void meleeAttack()
    {
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
        int rand = Random.Range(0, 2);
        if (rand == 1) 
        {
            FindObjectOfType<AudioManager>().Play("alphaAttack1");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("alphaAttack2");
        }
        
        GameObject createdAttack = Instantiate(meleePrefab, _transform.position, _transform.rotation);
        createdAttack.GetComponent<MeleeScript>().damage = damage;
        createdAttack.transform.position += createdAttack.transform.up / 2;
        createdAttack.transform.localScale += new Vector3(0.5f, 0.5f, 0);
        Destroy(createdAttack, 0.2f);
    }


    public void takeDamage(int damage)
    {
        Instantiate(effect_small, transform.position, Quaternion.identity);
        //health -= _player.GetComponent<ShipControl1>().playerAttack;
        foreach (var rend in GetComponentsInChildren<Renderer>(true))
        {
            rend.material.EnableKeyword("_EMISSION");
        }
        Invoke("resetFlash", 0.25f);
        //Time.timeScale = 0.025f;
        //Invoke("resetHitlag", 0.0025f);
        health -= damage;
        int rand = Random.Range(0, 2);
        if (rand == 1)
        {
            FindObjectOfType<AudioManager>().Play("alphadamage1");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("alphadamage2");
        }
        if (health <= 0)
        {
            onDeath();
        }

    }

    void onDeath()
    {
        _gameController.maxSpawned += 1;
        Instantiate(effect, transform.position, Quaternion.identity);
        GameObject createdBlood = Instantiate(bloodPrefab, _transform.position, _transform.rotation);
        createdBlood.transform.localScale += new Vector3(0.5f, 0.5f, 0);
        _gameController.score += 50;

        slainCount.GetComponent<KnightsSlain>().knightsSlain = _gameController.maxSpawned;
        Debug.Log(_gameController.maxSpawned);
        FindObjectOfType<AudioManager>().Play("alphadeath");
        GameObject createdPotion = Instantiate(potionPrefab, _transform.position, potionPrefab.transform.rotation);
        Time.timeScale = 1.0f;
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

    void resetFlash()
    {
        foreach (var rend in GetComponentsInChildren<Renderer>(true))
        {
            rend.material.DisableKeyword("_EMISSION");
        }
    }

    void resetHitlag()
    {
        Time.timeScale = 1.0f;
    }
}
