using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        int randSprite = Random.Range(0, 2);
        _spriteRenderer.sprite = sprites[randSprite];
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
