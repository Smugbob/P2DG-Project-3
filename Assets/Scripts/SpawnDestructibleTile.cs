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
    private int chance = 0;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned != true)
        {
            spawnEnemy();
        }
    }

    void spawnEnemy()
    {
        chance = Random.Range(0, 3);
        if (chance == 1)
        {
            int objType = Random.Range(0, 2);
            //objectPrefab = destructibles[objType];

            //instantiate object prefab with created coordinates and rotation
            if (objType == 0)
            {
                GameObject createdObject = Instantiate(objectPrefab1, _transform.position, _transform.rotation);
            }
            if (objType == 1)
            {
                GameObject createdObject = Instantiate(objectPrefab2, _transform.position, _transform.rotation);
            }
            
        }
        spawned = true;

    }
}
