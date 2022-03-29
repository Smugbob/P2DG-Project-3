using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyTile : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyAlphaPrefab;
    private Transform _ourTransform;
    private Camera _camera;
    public float spawnTime;
    public float spawnDelay;
    [SerializeField]
    [Range(0, 999)]
    private int spawnCount;
    private int rand = 0;
    private GameController _gameController;

    private void spawnEnemy()
    {
        //Check the game state, only spawn objects when the state is set to Playing
        if (_gameController.GetState() != GameController.EGameState.Playing)
            return;

        //get random rotation
        //Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        /*
        //get random coordinates within the view perspective of the camera
        Vector3 randomCamPoint = new Vector3(
        Random.Range(0, Screen.width),
        Random.Range(0, Screen.height),
        _camera.farClipPlane / 2);
        Vector3 randomWorldPos = _camera.ScreenToWorldPoint(randomCamPoint);
        */

        rand = Random.Range(0, 10);
        if (rand == 1)
        {
            //instantiate object prefab with created coordinates and rotation
            GameObject createdAlphaEnemy = Instantiate(enemyAlphaPrefab, _ourTransform.position, _ourTransform.rotation);
        }
        else
        {
            GameObject createdEnemy = Instantiate(enemyPrefab, _ourTransform.position, _ourTransform.rotation);
        }
            

        //decrement spawn count, override finite spawning if 999
        if (!(spawnCount == 999))
            spawnCount -= 1;

        //stop repeated function when count is done
        if (spawnCount <= 0)
            CancelInvoke("spawnEnemy");

    }
    // Start is called before the first frame update
    void Start()
    {
        //cached components
        _ourTransform = GetComponent<Transform>();
        _camera = Camera.main;
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        Debug.Assert(_camera, this.gameObject);
        _gameController.totalSpawned += (spawnCount / 2);



        //spawnRandomAsteroid called 'spawnTime' seconds after start, every 'spawnDelay' amount of seconds elapsed
        if (spawnCount != 0)
        {
            InvokeRepeating("spawnEnemy", spawnTime, spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
