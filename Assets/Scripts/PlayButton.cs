using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour
{
    private GameController _gameController;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeState()
    {
        _gameController.ChangeState(GameController.EGameState.Playing);
        _gameController.RestartGame();
        EventSystem.current.SetSelectedGameObject(null);
    }
}
