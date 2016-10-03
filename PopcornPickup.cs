using UnityEngine;
using System.Collections;

public class PopcornPickup : MonoBehaviour {

	public int speed;
	public GameObject targetPlayer;
	public bool magnetised = false;
	private Vector2 targetPos;

	void Update () {
		if (targetPlayer) {
			if (magnetised == true) {
				targetPos = new Vector2 (targetPlayer.transform.position.x, targetPlayer.transform.position.y);
				transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), targetPos, speed * Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Magnet") {
			if (GameManager.detached == true) {
				targetPlayer = other.gameObject;
				magnetised = true;
			}
		}
		if (other.gameObject.tag == "Mouth") {
			Destroy (this.gameObject);
		}
	}
}
