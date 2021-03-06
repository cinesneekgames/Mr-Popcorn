using UnityEngine;
using System.Collections;

public class DontGoThroughThings : MonoBehaviour
{
	// Pas op met het zetten naar true - zou dubbel kunnen maken
	// events die af moeten gaan - maar het gaat niet door de trigger
	public bool sendTriggerMessage = false; 	

	public LayerMask layerMask = -1; //zeker zijn dat we niet in deze layer zitten
	public float skinWidth = 0.1f; //hoeft niet veranderd te worden

	private float minimumExtent; 
	private float partialExtent; 
	private float sqrMinimumExtent; 
	private Vector2 previousPosition; 
	private Rigidbody2D myRigidbody;
	private Collider2D myCollider;
	private Vector2 hitCoordinates;

	//initialize values 
	void Start() 
	{ 
		myRigidbody = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<Collider2D>();
		previousPosition = myRigidbody.position; 
		minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z); 
		partialExtent = minimumExtent * (1.0f - skinWidth); 
		sqrMinimumExtent = minimumExtent * minimumExtent; 
	} 

	void FixedUpdate() 
	{ 
		//Meer afstand afgelegd dan de minimale omvang?
		Vector2 movementThisStep = myRigidbody.position - previousPosition; 
		float movementSqrMagnitude = movementThisStep.sqrMagnitude;

		if (movementSqrMagnitude > sqrMinimumExtent) 
		{ 
			float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
			RaycastHit hitInfo; 

			// controleren of er obstructies zijn die we hebben gemist
			if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
			{
				hitCoordinates = hitInfo.point;
				if (!hitInfo.collider)
					return;

				if (hitInfo.collider.isTrigger) 
					hitInfo.collider.SendMessage("OnTriggerEnter", myCollider);

				if (!hitInfo.collider.isTrigger)
					myRigidbody.position = hitCoordinates - (movementThisStep / movementMagnitude) * partialExtent; 

			}
		} 

		previousPosition = myRigidbody.position; 
	}
}
