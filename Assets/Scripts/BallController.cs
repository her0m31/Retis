using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public float speed = 100.0f;
	private Rigidbody2D ball;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Rigidbody2D>();
		ball.AddForce(Vector2.down*speed);
	}

	void Update() {

	}

	void OnCollisionEnter2D(Collision2D coll) {
		Vector2 vel = ball.velocity;
		ball.AddForce(coll.gameObject.transform.up, ForceMode2D.Impulse);
		ball.velocity = ball.velocity.normalized*2.0f;
	}
}
