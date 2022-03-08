using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    [SerializeField]
    private Sprite[] asteroidSpritesLarge;
    private SpriteRenderer _spriteRenderer;
    private float xspeed;
    private float yspeed;
    private Transform _transform;
    private Rigidbody2D _RB;
    [SerializeField]
    private GameObject asteroidSmall;
    /*
    private enum AsteroidType
    {
        Large ,
        Small
    }
    private AsteroidType asteroidT = AsteroidType.Large;
    */
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _RB = GetComponent<Rigidbody2D>();
        xspeed = Random.Range(20000, 30000);
        yspeed = Random.Range(20000, 30000);

        ChangeSprite();

        Vector2 translation;
        translation.x = xspeed * Time.deltaTime;
        translation.y = yspeed * Time.deltaTime;

        _RB.AddForce(transform.up * translation.y);
        _RB.AddForce(transform.right * translation.x);
    }

    // Update is called once per frame
    void Update()
    {
        
        // _transform.Translate(Vector3.up * translation.y);
        //  _transform.Translate(Vector3.right * translation.x);

    }

    void ChangeSprite()
    {
       int randSprite = Random.Range(0, 3);
       /* if (this.asteroidT == AsteroidType.Large)*/ _spriteRenderer.sprite = asteroidSpritesLarge[randSprite];
       // if (this.asteroidT == AsteroidType.Small) _spriteRenderer.sprite = asteroidSpritesSmall[randSprite];
       Destroy(GetComponent<PolygonCollider2D>());
       gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
    }

    void spawnNewAsteroid()
    {
        GameObject smallAsteroid1 = Instantiate(asteroidSmall, _transform.position, _transform.rotation);
        //smallAsteroid1.GetComponent<AsteroidScript>().asteroidT = AsteroidType.Small;
       // smallAsteroid1.GetComponent<AsteroidScript>().ChangeSprite();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            
          // if (asteroidT == AsteroidType.Large)
          // {
             Destroy(other.gameObject);
             spawnNewAsteroid();
             spawnNewAsteroid();
             Debug.Log("Hit");
             Destroy(gameObject);
                


                //Destroy(other);
           // }
          // if (asteroidT == AsteroidType.Small) Destroy(gameObject);
        }
    }
}
