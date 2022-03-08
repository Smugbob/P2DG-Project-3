using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController _gameController = null;

    public enum EGameState
    {
        MainMenu ,
        Playing ,
        Paused ,
        Gameover
    }
    private EGameState _eGameState = EGameState.Playing;

    void Awake()
    {
        //Assert to verify only one game controller exists
        Debug.Assert(_gameController == null, this.gameObject);

        //Assign our static reference to this one we just created
        _gameController = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (_eGameState)
        {
            case EGameState.MainMenu:
                break;

            case EGameState.Playing:
                if (Input.GetKeyDown(KeyCode.Escape))
                    ChangeState(EGameState.Paused);
                break;

            case EGameState.Paused:
                if (Input.GetKeyDown(KeyCode.Escape))
                    ChangeState(EGameState.Playing);
                break;

            case EGameState.Gameover:
                break;


            default:
                throw new System.ArgumentOutOfRangeException();
        }



    }
    public void ChangeState(EGameState eGameState)
    {
        Debug.Log("#Change State - " + eGameState);
        switch (eGameState)
        {
            ////////////////////////////////////////////////////////////////
            case EGameState.MainMenu:
                break;
            ////////////////////////////////////////////////////////////////
            case EGameState.Playing:
                Time.timeScale = 1.0f;
                break;
            ////////////////////////////////////////////////////////////////
            case EGameState.Gameover:
                break;
            ////////////////////////////////////////////////////////////////
            case EGameState.Paused:
                Time.timeScale = 0.0f;
                break;
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            // The game is in an invalid state
            default:
                throw new System.ArgumentOutOfRangeException();
                ////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////
        }
        // Set our state to the new state
        _eGameState = eGameState;
    }
}
