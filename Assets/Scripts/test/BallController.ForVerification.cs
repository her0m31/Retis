using UnityEngine;
using System.Collections;

public class BallControllerFV : MonoBehaviour {
	private float speed = 5.0f;
	// private float addSpeed = 1.0f;
	private Rigidbody2D ball;
	// 移動ベクトル
	private Vector2 vectorV;

	void OnCollisionEnter2D(Collision2D coll) {
		// 法線ベクトルをボールに適用 -> 緑軸の方向へ
		// Vector2 vel = coll.gameObject.transform.up * speed;
		// ball.velocity = vel;
		Debug.Log(ball.velocity);

		// 法線ベクトル N
		Vector2 vectorN = coll.contacts[0].normal;
		Debug.Log("vectorN: "+ vectorN);
		Debug.Log("vectorN.x^2: "+ vectorN.x*vectorN.x);
		Debug.Log("vectorN.y^2: "+ vectorN.y*vectorN.y);
		// Nの正規化 -> ベクトルの平方根の長さ (x*x + y*y)
		float magnitude = vectorN.sqrMagnitude;
		Debug.Log("mag: "+ magnitude);
		Vector2 normalizedN = new Vector2(vectorN.x/magnitude, vectorN.y/magnitude);
		Debug.Log("normalizedN: "+ normalizedN);
		// 投影ベクトル Pを求めたい -> vectorV と Nの内積を求める
		float dot = -1.0f*vectorV.x * normalizedN.x + -1.0f*vectorV.y * normalizedN.y;
		Debug.Log("dot: "+ dot);
		Vector2 vectorP = new Vector2(normalizedN.x * dot, normalizedN.y * dot);
		Debug.Log("vectorP: "+ vectorP);
		// 移動(反射)ベクトル
		Vector2 vectorX = vectorV + 2.0f*vectorP;
		Debug.Log("vectorX: "+ vectorX);
		// ボールに移動ベクトルを適用
		ball.velocity = vectorX;

		// if(addSpeed < 5.0f) {
		// 	// 衝突毎に加速させたいので * 1.01f
		// 	addSpeed *= 1.01f;
		// }
		// 反射ベクトルを次の衝突時の入射ベクトルとして保持
		vectorV = ball.velocity ;
		Debug.Log("vectorV: "+ vectorV);
	}

	void Start () {
		ball = GetComponent<Rigidbody2D>();
		vectorV = new Vector2(-1.0f, -1.0f)*speed;
		ball.velocity = vectorV;
	}
}
