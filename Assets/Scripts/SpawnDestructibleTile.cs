using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestructibleTile : MonoBehaviour
{
    //[SerializeField]
   // private GameObject[] destructibles;
    private Transform _transform;
    private GameController _gameController;
    [SerializeField]
    private GameObject objectPrefab1;
    [SerializeField]
    private GameObject objectPrefab2;
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
            spawnDestructible();
        }
    }

    void spawnDestructible()
    {
        //50% chance to spawn object on tile
        rand = Random.Range(0, 2);
        if (rand == 1)
        {
            //two types of objects picked randomly between
            int objType = Random.Range(0, 2);
            

            //instantiate object prefab with created coordinates and rotation
            if (objType == 0)
            {
                GameObject createdObject = Instantiate(objectPrefab1, _transform.position, _transform.rotation);
                //object's spawned halfway inside the ground, move back onto ground when spawned
                createdObject.transform.position = new Vector3(createdObject.transform.position.x, createdObject.transform.position.y, createdObject.transform.position.z - 0.35f);
            }
            if (objType == 1)
            {
                GameObject createdObject = Instantiate(objectPrefab2, _transform.position, _transform.rotation);
                createdObject.transform.position = new Vector3(createdObject.transform.position.x, createdObject.transform.position.y, createdObject.transform.position.z - 0.35f);
            }
            
        }
        spawned = true;

    }
}
