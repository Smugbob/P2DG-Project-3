using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health_System : MonoBehaviour
{
    public int total_health = 100;
    public int current_health = 0;
    public int healthOpposite = 0;

    public Healthbar healthBar;
    private GameController _gameController;
    private GameObject _mainMenu;
    public GameObject effect_small;

    // Start is called before the first frame update
    void Start()
    {
        current_health = total_health;
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        _mainMenu = GameObject.Find("MainMenu");
        SceneManager.SetActiveScene(gameObject.scene);
    }

    // Update is called once per frame
    void Update()
    {
        if (current_health > total_health)
        {
            current_health = total_health;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("damage taken");
            take_damage(10);
            

        }
        */

    }

    public void take_damage(int damage)
    {
        Instantiate(effect_small, transform.position, Quaternion.identity);
        current_health -= damage;
        healthOpposite += damage;
        healthBar.set_health(current_health);
        foreach (var rend in GetComponentsInChildren<Renderer>(true))
        {
            rend.material.EnableKeyword("_EMISSION");
        }
        Invoke("resetFlash", 0.25f);
        if ((current_health <= 10) && (current_health > 0))
        { 
            FindObjectOfType<AudioManager>().Play("healthlow");
        }
        int rand = Random.Range(0, 2);
        if (rand == 1)
        {
            FindObjectOfType<AudioManager>().Play("playerdamage1");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("playerdamage2");
        }
        if (current_health <= 0)
        {
            Debug.Log("You have died");
            FindObjectOfType<AudioManager>().Play("playerdeath");
            //current_health = 100;
            _gameController.ChangeState(GameController.EGameState.Gameover);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if ((other.gameObject.tag == "Potion"))
        {
            Debug.Log("heal");
            FindObjectOfType<AudioManager>().Play("potionpickup");
            take_damage(-30);
            Destroy(other.gameObject);
        }
        
    }

    void resetFlash()
    {
        foreach (var rend in GetComponentsInChildren<Renderer>(true))
        {
            rend.material.DisableKeyword("_EMISSION");
        }
    }
}

///test comment ignore