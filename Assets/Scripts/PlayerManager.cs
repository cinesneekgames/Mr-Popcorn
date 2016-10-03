using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public float speed;
	
	static public Rigidbody2D rb;
	private GameManager gameManagerScript;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		gameManagerScript = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	void FixedUpdate() {
		if (!GameManager.detached) {
			float x = Input.GetAxisRaw ("Horizontal");

			rb.velocity = new Vector2 (speed * x * Time.fixedDeltaTime, rb.velocity.y);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Killzone") {
			gameManagerScript.RespawnPlayer ();
			Destroy (this.gameObject);
		}
	}
}
