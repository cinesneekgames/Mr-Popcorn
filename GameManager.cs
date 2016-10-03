using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject player;
	public Transform spawn;

	static public bool detached;

	void Update () {
		detachClaw ();
	}

	void detachClaw () {
		if (!detached) {
			if (Input.GetKeyDown ("space")) {
				print ("Dropped!");
				detached = true;
				PlayerManager.rb.gravityScale = 1;
				PlayerManager.rb.isKinematic = false;
			}
		}
	}

	public void RespawnPlayer() {
		Instantiate (player, spawn.position, spawn.rotation);
		detached = false;
	}
}
