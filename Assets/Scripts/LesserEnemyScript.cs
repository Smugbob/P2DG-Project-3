using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserEnemyScript : MonoBehaviour
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
    private float minDist = 1.0f;
    private int rand = 0;
    [SerializeField]
    private int damage = 1;
    private bool attacking = false;
    private GameObject slainCount;
    public GameObject effect;
    public GameObject effect_small;
    public GameObject bloodPrefab;

    private enum attackType
    {
        Melee,
        Ranged
    }
    private attackType attack;

    // Start is called before the first frame update
    void Start()
    {
        //cache components
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        slainCount = GameObject.Find("KnightsSlain");
        _player = GameObject.Find("Player");
        _transform = GetComponent<Transform>();
        _playerTransform = _player.GetComponent<Transform>();
        _RB = GetComponent<Rigidbody2D>();
        direction = transform.TransformDirection(Vector2.up);

        rand = Random.Range(0, 4);
        if (rand == 1)
        {
            attack = attackType.Ranged;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            attack = attackType.Melee;
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
        }

        if (attack == attackType.Melee)
        {
            minDist = 3.0f;
            //InvokeRepeating("meleeAttack", attackTime, attackRate);
        }
        else
        {
            minDist = 4.0f;
            health = 20;
            //InvokeRepeating("rangedAttack", attackTime * 2, attackRate);
        }

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
        if ((Vector3.Distance(_playerTransform.position, _transform.position) < minDist)) {
            attacking = true; 

            if (attack == attackType.Melee)
            {
                meleeAttack();
                Invoke("changeBackModel", 1.0f);
            }

            if (attack == attackType.Ranged)
            {
                rangedAttack();
                transform.GetChild(3).gameObject.SetActive(false);
                Invoke("changeBackSpear", 1.0f);
            }
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

        if (!((attack == attackType.Ranged) && ((Vector3.Distance(_playerTransform.position, _transform.position) < minDist))))
        {
            _transform.position = Vector2.MoveTowards(_transform.position, _playerTransform.position, step);
        }
        
        
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
        int rand = Random.Range(0, 2);
        if (rand == 1)
        {
            FindObjectOfType<AudioManager>().Play("meleeattack1");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("meleeattack2");
        }
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        createdAttack = Instantiate(meleePrefab, _transform.position, _transform.rotation);
        createdAttack.GetComponent<MeleeScript>().damage = damage;
        createdAttack.transform.position += createdAttack.transform.up / 2;
        Destroy(createdAttack, 0.2f);
    }

    void rangedAttack()
    {
        //get the distance between the enemy and the player
        float xdist = _transform.position.x - _playerTransform.position.x;
        float ydist = _transform.position.y - _playerTransform.position.y;

        xdist = Mathf.Abs(xdist);
        ydist = Mathf.Abs(ydist);
        GameObject createdAttack;

        int rand = Random.Range(0, 2);
        if (rand == 1)
        {
            FindObjectOfType<AudioManager>().Play("rangedattack1");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("rangedattack2");
        }
        //Quaternion rotation = Quaternion.Euler(getDirection()[0] + 90, 90, 90);
        createdAttack = Instantiate(rangedPrefab, _transform.position, _transform.rotation);
        createdAttack.GetComponent<LaserProjectile>().damage = damage / 2;
        //Destroy(createdAttack, 5);
    }

    public void takeDamage(int damage)
    {

        //health -= _player.GetComponent<ShipControl1>().playerAttack;
        Instantiate(effect_small, transform.position, Quaternion.identity);
        health -= damage;
        foreach (var rend in GetComponentsInChildren<Renderer>(true))
        {
            rend.material.EnableKeyword("_EMISSION");
        }
        Invoke("resetFlash", 0.25f);
        //Time.timeScale = 0.025f;
        //Invoke("resetHitlag", 0.0025f);
        if (attack == attackType.Melee) 
        {
            int rand = Random.Range(0, 2);
            if (rand == 1)
            {
                FindObjectOfType<AudioManager>().Play("meleedamage1");
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("meleedamage2");
            }
        }
        else
        {
            int rand = Random.Range(0, 2);
            if (rand == 1)
            {
                FindObjectOfType<AudioManager>().Play("rangeddamage1");
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("rangeddamage2");
            }
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
        if (attack == attackType.Melee)
        {
            FindObjectOfType<AudioManager>().Play("meleedeath");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("rangeddeath");
        }
        _gameController.score += 10;
        slainCount.GetComponent<KnightsSlain>().knightsSlain = _gameController.maxSpawned;
        Debug.Log(_gameController.maxSpawned);
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

    void changeBackSpear()
    {
        transform.GetChild(3).gameObject.SetActive(true);
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
