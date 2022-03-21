using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipControl1 : MonoBehaviour
{
	[SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;
	private Camera _Camera;
	[SerializeField] private GameObject Laser;

	private void Awake()
	{

	}

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
		

		// Add force to the upwards direction of the object * speed * input [-1,1]
		//_shipRB.AddForce(transform.up * translation);
		// Rotate around our z-axis
		//_shipTransform.Rotate(0, 0, -rotation);

		//camera follows player
		Vector3 camera_pos = new Vector3(transform.position.x, transform.position.y, _Camera.transform.position.z);
		_Camera.transform.position = camera_pos;

		//spawn laser
		/*if (Input.GetKeyDown(KeyCode.Space))
        {
			GameObject CreatedLaser = Instantiate(Laser, transform.position, transform.rotation);
		}*/
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
	}
}

