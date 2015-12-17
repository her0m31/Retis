using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public float speed = 100.0f;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce(Vector2.down*speed);
	}

	void Update() {
	}

}
