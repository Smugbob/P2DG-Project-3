using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private static GameController _gameController = null;
    public int score = 0;
    private Camera _Camera;
    [SerializeField]
    public int totalSpawned = 0; //max number of enemies that will be spawned
    

    public enum EGameState
    {
        MainMenu ,
        Playing ,
        Paused ,
        Gameover,
        Win
    }
    private EGameState _eGameState = EGameState.MainMenu; //game will begin at the main menu

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
        _Camera = Camera.main; //_Camera cached as the primary one
        GameObject.Find("HUD").GetComponent<Canvas>().enabled = false; //hide the UI whilst in the main menu
    }

    // Update is called once per frame
    void Update()
    {

       
        switch (_eGameState)
        { //checks made every frame depending on the current game state
            case EGameState.MainMenu:
                _Camera.transform.position = new Vector3(50, 0, -10); //move camera to main menu screen
                if (Input.GetKeyDown(KeyCode.P))
                {
                    RestartGame(); //refresh game scene
                    ChangeState(EGameState.Playing);
                }
                break;

            case EGameState.Playing:
                if (Input.GetKeyDown(KeyCode.Escape))
                    ChangeState(EGameState.Paused); //pause the game when escape is pressed
                if (totalSpawned <= 0)
                {
                    ChangeState(EGameState.Win); //win when there are no more enemies remaining
                }
                break;

            case EGameState.Paused:
                if (Input.GetKeyDown(KeyCode.Escape))
                    ChangeState(EGameState.Playing); //resume back to playing if escape is pressed again
                break;

            case EGameState.Gameover:
                _Camera.transform.position = new Vector3(70, 0, -10); //move camera to game over screen
                if (Input.GetKeyDown(KeyCode.P))
                {
                    ChangeState(EGameState.MainMenu);
                }
                break;

            case EGameState.Win:
                _Camera.transform.position = new Vector3(90, 0, -10); //move camera to win screen
                if (Input.GetKeyDown(KeyCode.P))
                {
                    ChangeState(EGameState.MainMenu);
                }
                break;


            default:
                throw new System.ArgumentOutOfRangeException(); //raise exception if game is somehow in a state different to the aforementioned
        }


    }
    public void ChangeState(EGameState eGameState)
    {
        Debug.Log("#Change State - " + eGameState);
        switch (eGameState) //statements to be run once depending on the game state
        {
            ////////////////////////////////////////////////////////////////
            case EGameState.MainMenu:
                GameObject.Find("HUD").GetComponent<Canvas>().enabled = false; //hide the UI whilst in the main menu
                GameObject.Find("Buttons").GetComponent<Canvas>().enabled = true; 
                Time.timeScale = 0.0f; //flow of time in the game is multiplied by 0, consequently stopping anything from moving or spawning
                break;
            ////////////////////////////////////////////////////////////////
            case EGameState.Playing:
                GameObject.Find("HUD").GetComponent<Canvas>().enabled = true;
                Time.timeScale = 1.0f; //resume flow of time
                break;
            ////////////////////////////////////////////////////////////////
            case EGameState.Gameover:
                GameObject.Find("HUD").GetComponent<Canvas>().enabled = false;
                Time.timeScale = 0.0f;
                break;
            ////////////////////////////////////////////////////////////////
            case EGameState.Win:
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
        
        score = 0; //reset the score


    }
    public EGameState GetState()
    {
        return _eGameState;
    }
}
