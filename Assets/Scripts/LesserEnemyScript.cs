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

    // Start is called before the first frame update
    void Start()
    {
        //cache components
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        _player = GameObject.Find("Player");
        _transform = GetComponent<Transform>();
        _playerTransform = _player.GetComponent<Transform>();
        _RB = GetComponent<Rigidbody2D>();
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

    void attack()
    {

    }

    void takeDamage()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    }
}
