using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;
    private Transform _ourTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private Sprite[] asteroidSprites;

    // Start is called before the first frame update
    void Start()
    {
        _ourTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug Test
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnRandomAsteroid();
        }
    }

    private void SpawnRandomAsteroid()
    {
        // Get a random rotation around z between 0-360 degrees
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        Vector3 randomCamPoint = new Vector3(
        Random.Range(0, Screen.width),
        Random.Range(0, Screen.height),
        _camera.farClipPlane / 2);

        // Turn this into a position in the world
        Vector3 randomWorldPos = _camera.ScreenToWorldPoint(randomCamPoint);
        // Spawn an instance of our asteroid prefab and retain a reference
        GameObject createdAsteroid = Instantiate(asteroidPrefab, randomWorldPos, randomRotation);

        // Turn this into a position in the world
        randomWorldPos = _camera.ScreenToWorldPoint(randomCamPoint);

        //Getting the sprite component after you've created the asteroid
        createdAsteroid.GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, 7)];
    }


}
