using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScriptSmall : MonoBehaviour
{
    [SerializeField]
    private Sprite[] asteroidSpritesSmall;
    private SpriteRenderer _spriteRenderer;
    private float xspeed;
    private float yspeed;
    private Transform _transform;
    private Rigidbody2D _RB;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _RB = GetComponent<Rigidbody2D>();
        xspeed = Random.Range(-2000, 2000);
        yspeed = Random.Range(-2000, 2000);

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
        
    }

    void ChangeSprite()
    {
        int randSprite = Random.Range(0, 3);
        /* if (this.asteroidT == AsteroidType.Large)*/
        _spriteRenderer.sprite = asteroidSpritesSmall[randSprite];
        // if (this.asteroidT == AsteroidType.Small) _spriteRenderer.sprite = asteroidSpritesSmall[randSprite];
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {

            // if (asteroidT == AsteroidType.Large)
            // {
            Destroy(other.gameObject);
            Debug.Log("Hit");
            Destroy(gameObject);



            //Destroy(other);
            // }
            // if (asteroidT == AsteroidType.Small) Destroy(gameObject);
        }
    }
}


