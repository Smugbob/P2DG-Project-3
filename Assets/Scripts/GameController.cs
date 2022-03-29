using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private static GameController _gameController = null;
    public int score = 0;
    private Camera _Camera;
    

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
        _Camera = Camera.main;
        ChangeState(EGameState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_eGameState)
        {
            case EGameState.MainMenu:
                _Camera.transform.position = new Vector3(50, 0, -10);
                if (Input.GetKeyDown(KeyCode.P))
                {
                    RestartGame();
                    ChangeState(EGameState.Playing);
                }
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
                _Camera.transform.position = new Vector3(70, 0, -10);
                if (Input.GetKeyDown(KeyCode.P))
                {
                    ChangeState(EGameState.MainMenu);
                }
                break;


            default:
                throw new System.ArgumentOutOfRangeException();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

    }
    public void ChangeState(EGameState eGameState)
    {
        Debug.Log("#Change State - " + eGameState);
        switch (eGameState)
        {
            ////////////////////////////////////////////////////////////////
            case EGameState.MainMenu:
                GameObject.Find("HUD").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Buttons").GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0.0f;
                break;
            ////////////////////////////////////////////////////////////////
            case EGameState.Playing:
                GameObject.Find("HUD").GetComponent<Canvas>().enabled = true;
                Time.timeScale = 1.0f;
                break;
            ////////////////////////////////////////////////////////////////
            case EGameState.Gameover:
                GameObject.Find("HUD").GetComponent<Canvas>().enabled = false;
                Time.timeScale = 0.0f;
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

    public void RestartGame()
    {

        //Check whether there is already an active Game scene. If so, unload it.
        if (SceneManager.sceneCount > 1)
        {
            Debug.Log("Unloading Game Scene");
            SceneManager.UnloadSceneAsync("Game");
        }

        //Load a new game scene, resetting all assets within it
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);


    }
    public EGameState GetState()
    {
        return _eGameState;
    }
}
