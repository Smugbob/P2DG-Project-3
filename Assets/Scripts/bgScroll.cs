using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgScroll : MonoBehaviour
{
    [SerializeField]
    private float xspeed;
    private float origPosX;
    private GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        //cache components
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        origPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.GetState() != GameController.EGameState.Playing)
            return;
        float translation = xspeed * Time.deltaTime;
        transform.Translate(new Vector3(translation, 0, 0));

        if (transform.position.x > 35)
        {
            transform.position = new Vector3(-35, transform.position.y, transform.position.z);
        }
    }
}
