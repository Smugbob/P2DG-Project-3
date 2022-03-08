using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipControl : MonoBehaviour
{
	private Transform _shipTransform;
	private Rigidbody2D _shipRB;
	[SerializeField] 
	private float movementSpeed;
	[SerializeField] 
	private float rotationSpeed;
	[SerializeField]
	private GameObject bulletPrefab;

	private void Awake()
	{
		
	}
	
	void Start ()
	{
		_shipTransform = GetComponent<Transform>();
		_shipRB = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		float origSpeed = movementSpeed;
		if (Input.GetButton("Slow"))
		{
			origSpeed /= 2;
		}
		float translation = Input.GetAxis("Vertical") * origSpeed * Time.deltaTime;
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
		
		if (Input.GetKeyDown(KeyCode.Space))
        {

			GameObject createdBullet = Instantiate(bulletPrefab, _shipTransform.position, _shipTransform.rotation);
        }

		_shipRB.AddForce(transform.up * translation);
		_shipTransform.Rotate(0, 0, -rotation);
	}

}

