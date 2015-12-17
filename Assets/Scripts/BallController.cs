using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	private float speed = 5.0f;
	private Rigidbody2D ball;
	private Vector2 vel;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Rigidbody2D>();
		vel = new Vector2(0.0f, -1.0f)*speed;
		ball.velocity = vel;
	}

	void Update() {

	}

	void OnCollisionEnter2D(Collision2D coll) {
		// Vector2 vel = coll.gameObject.transform.up * speed;
		// ball.velocity = vel;

		Debug.Log(ball.velocity);

		// 法線ベクトル N
		Vector2 vectorN = coll.gameObject.transform.up;
		Debug.Log("vectorN: "+ vectorN);
		// Nの正規化
		Debug.Log("vectorN.x^2: "+ vectorN.x*vectorN.x);
		Debug.Log("vectorN.y^2: "+ vectorN.y*vectorN.y);
		float magnitude = (vectorN.x * vectorN.x) + (vectorN.y * vectorN.y);
		Debug.Log("mag: "+ magnitude);
		Vector2 normalizedN = new Vector2(vectorN.x/magnitude, vectorN.y/magnitude);
		Debug.Log("normalizedN: "+ normalizedN);
		// 投影ベクトル Pを求めたい -> vectorV と Nの内積を求める
		float dot = Vector2.Dot(-1.0f*vel, normalizedN);
		Debug.Log("dot: "+ dot);
		// 投影ベクトル P
		Vector2 vectorP = new Vector2(normalizedN.x * dot, normalizedN.y * dot);
		Debug.Log("vectorP: "+ vectorP);

		Vector2 vectorV = vel + vectorP;
		Debug.Log("vectorV: "+ vectorV);

		Vector2 vectorX = vectorV + vectorP;
		Debug.Log("vectorX: "+ vectorX);

		ball.velocity = vectorX;
		vel = ball.velocity;
	}
}
