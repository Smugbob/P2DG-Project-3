using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject asteroidPrefab;
    private Transform _ourTransform;
    [SerializeField]
    private Camera _camera;
    //public bool stopSpawning = false;
    private bool leftOrRight = false;
    public float spawnTime;
    public float spawnDelay;
    public float spawnCount;

    private void spawnRandomAsteroid()
    {
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Vector3 randomCamPoint = new Vector3(
        Random.Range(0, Screen.width),
        Random.Range(0, Screen.height),
        _camera.farClipPlane / 2);        Vector3 randomWorldPos = _camera.ScreenToWorldPoint(randomCamPoint);        GameObject createdAsteroid = Instantiate(asteroidPrefab, randomWorldPos, randomRotation);
        if (!(spawnCount == 999))
            spawnCount -= 1;

        if (spawnCount <= 0)// stopSpawning = true;
            CancelInvoke("spawnRandomAsteroid");
        /*
        if (stopSpawning)
        {
            CancelInvoke("spawnRandomAsteroid");
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        _ourTransform = GetComponent<Transform>();
        InvokeRepeating("spawnRandomAsteroid", spawnTime, spawnDelay);
    }

    //public void spawn
    /*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            spawnRandomAsteroid();
        }
    }
    */


}
