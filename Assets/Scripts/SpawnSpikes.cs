using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpikes : MonoBehaviour
{
    //[SerializeField]
    // private GameObject[] destructibles;
    private Transform _transform;
    private GameController _gameController;
    [SerializeField]
    private GameObject objectPrefab;
    private int rand = 0;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        //cache components
        _transform = GetComponent<Transform>();
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();


    }

    // Update is called once per frame
    void Update()
    {
        //allows to be run in game scene, but only once
        if (spawned != true)
        {
            spawnSpike();
        }
    }

    void spawnSpike()
    {
        //50% chance to spawn object on tile
        rand = Random.Range(0, 2);
        if (rand == 1)
        {


            if (_gameController.GetState() != GameController.EGameState.Playing)
                return;
            //instantiate object prefab with created coordinates and rotation

           
            GameObject createdObject = Instantiate(objectPrefab, _transform.position, _transform.rotation);
            //object's spawned halfway inside the ground, move back onto ground when spawned
            createdObject.transform.position = new Vector3(createdObject.transform.position.x, createdObject.transform.position.y, createdObject.transform.position.z - 0.1f);


        }
        spawned = true;

    }
}

