using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	private float speed = 5.0f;
	private Rigidbody2D ball;
	private Vector2 vel;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Rigidbody2D>();
		vel = new Vector2(-1.0f, -1.0f)*speed;
		ball.velocity = vel;
	}

	void Update() {

	}

	void OnCollisionEnter2D(Collision2D coll) {
		// Vector2 vel = coll.gameObject.transform.up * speed;
		// ball.velocity = vel;

		// 法線ベクトル N
		Vector2 vectorN = coll.gameObject.transform.up;
		// Nの正規化
		float magnitude = Mathf.Sqrt(vectorN.x * vectorN.x + vectorN.y * vectorN.y);
		Vector2 normalizedN = new Vector2(vectorN.x/magnitude, vectorN.y/magnitude);
		// 投影ベクトル Pを求めたい -> vectorV と Nの内積を求める
		float dot = -1.0f*vel.x * normalizedN.x + -1.0f*vel.y * normalizedN.y;
		// 投影ベクトル P
		Vector2 vectorP = new Vector2(normalizedN.x * dot, normalizedN.y * dot);
		Vector2 vectorX = vel + 2.0f*vectorP;

		ball.velocity = vel + 2.0f*vectorP;
		vel = ball.velocity;
	}
}
