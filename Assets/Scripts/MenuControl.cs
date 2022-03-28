using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{

    private GameController _gameController;
    [SerializeField]
    private Sprite[] menuSprites;
    private SpriteRenderer _spriteRenderer;
    private GameObject _player;
    public Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.Find("Player");
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_gameController.GetState())
        {
            case GameController.EGameState.MainMenu:
                _spriteRenderer.sprite = menuSprites[0];
                break;

            case GameController.EGameState.Playing:
                transform.position = new Vector3(100, 100, 0);
                break;

            case GameController.EGameState.Gameover:
                _spriteRenderer.sprite = menuSprites[1];
                transform.position = lastPos;
                break;


            default:
                break;
        }
    }
}
