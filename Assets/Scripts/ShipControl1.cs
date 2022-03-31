using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipControl1 : MonoBehaviour
{
	[SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;
	private Camera _Camera;
	[SerializeField] private GameObject Laser;
	[SerializeField] private GameObject meleePrefab;
	public int playerAttack = 10;
	bool intangible = false;
	bool attacking = false;
	

	
	// Cached component references
	private Transform _shipTransform;
	private Rigidbody2D _shipRB;
	private GameController _gameController;
	

	void Start()
	{
		// Cache components we need here
		_gameController = GameObject.Find("GameManager").GetComponent<GameController>();
		_shipTransform = transform;
		_shipRB = GetComponent<Rigidbody2D>();
		_Camera = Camera.main;

		transform.GetChild(1).gameObject.SetActive(false);
		transform.GetChild(2).gameObject.SetActive(false);



	}

	void Update()
	{
		//Only update if the current game state is Playing
		if (_gameController.GetState() != GameController.EGameState.Playing)
			return;

		// Get the vertical axis.
		// The value is in the range -1 to 1
		float translationy = Input.GetAxis("Vertical") * movementSpeed;
		float translationx = Input.GetAxis("Horizontal") * movementSpeed;

		float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

		// Make it move x meters per second instead of x meters per frame
		translationx *= Time.deltaTime;
		translationy *= Time.deltaTime;

		Vector2 moveposition = _shipTransform.position + new Vector3(translationx, translationy);
		_shipRB.MovePosition(moveposition);

		// Rotate around our z-axis
		_shipTransform.Rotate(0, 0, -rotation);

		//camera follows player
		Vector3 camera_pos = new Vector3(transform.position.x, transform.position.y, _Camera.transform.position.z);
		_Camera.transform.position = camera_pos;

		//spawn laser
		if (Input.GetKeyDown(KeyCode.X))
		{
			if (attacking == false)
			{
				int rand = Random.Range(0, 2);
				if (rand == 1)
				{
					FindObjectOfType<AudioManager>().Play("playerranged1");
				}
				else
				{
					FindObjectOfType<AudioManager>().Play("playerranged2");
				}
				transform.GetChild(0).gameObject.SetActive(false);
				transform.GetChild(1).gameObject.SetActive(false);
				transform.GetChild(2).gameObject.SetActive(true);
				Invoke("changeBackModel", 0.5f);
				GameObject CreatedLaser = Instantiate(Laser, transform.position, transform.rotation);
				CreatedLaser.GetComponent<LaserProjectile>().damage = 10;
				attacking = true;
				Invoke("delayAttack", 0.5f);
			}
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (attacking == false)
            {
				int rand = Random.Range(0, 2);
				if (rand == 1)
				{
					FindObjectOfType<AudioManager>().Play("playerattack1");
				}
				else
				{
					FindObjectOfType<AudioManager>().Play("playerattack2");
				}
				transform.GetChild(0).gameObject.SetActive(false);
				transform.GetChild(1).gameObject.SetActive(true);
				transform.GetChild(2).gameObject.SetActive(false);
				Invoke("changeBackModel", 0.39f);
				GameObject CreatedMelee = Instantiate(meleePrefab, transform.position, transform.rotation);
				CreatedMelee.GetComponent<MeleeScript>().damage = 10;
				//CreatedMelee.transform.position += CreatedMelee.transform.up;
				Destroy(CreatedMelee, 0.4f);
				attacking = true;
				Invoke("delayAttack", 0.4f);
			}
			
		}

		//player faces direction of movement

		
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			Vector3 up_pos = new Vector3(0, 0, 0);
			transform.eulerAngles = up_pos;
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			Vector3 left_pos = new Vector3(0, 0, 90);
			transform.eulerAngles = left_pos;
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			Vector3 down_pos = new Vector3(0, 0, 180);
			transform.eulerAngles = down_pos;
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			Vector3 right_pos = new Vector3(0, 0, -90);
			transform.eulerAngles = right_pos;
		}
		


	}

	void delayAttack()
    {
		attacking = false;
    }

	void changeBackModel()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(1).gameObject.SetActive(false);
		transform.GetChild(2).gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
	}
}

