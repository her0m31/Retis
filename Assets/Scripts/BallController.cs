using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	private float speed = 5.0f;
	private Rigidbody2D physicsBall;
	private Vector2 inDirection;
	private GameObject outEffect;
	private GameObject hitEffect;

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
				enabled = false;
				break;
			case GameManager.GameState.GameOver:
				enabled = false;
				break;
			case GameManager.GameState.Playing:
				enabled = true;
				break;
		}
	}

	void EffectActivation(Collision2D coll) {
		if(coll.gameObject.CompareTag("OutOfArea") && GameManager.State.Value == GameManager.GameState.Playing) {
			GameObject.Instantiate(outEffect, coll.contacts[0].point, Quaternion.identity);
			GameManager.State.Value = GameManager.GameState.GameOver;
			Destroy(gameObject);
		}
		else {
			GameObject.Instantiate(hitEffect, coll.contacts[0].point, Quaternion.identity);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// 正規化された法線ベクトルを取得
		Vector2 inNormal = coll.contacts[0].normal;
		// vectorV => 入射ベクトル
		physicsBall.velocity = Vector2.Reflect(inDirection, inNormal);
		// 反射ベクトルを次の入射ベクトルとして保持
		inDirection = physicsBall.velocity;
		// 衝突エフェクト発生
		EffectActivation(coll);
	}

	void Awake() {
		outEffect = GameObject.Find("DeathEffect");
		hitEffect = GameObject.Find("SuccesEffects");

		physicsBall = gameObject.GetComponent<Rigidbody2D>();
		physicsBall.velocity = new Vector2(-0.5f, -1.0f)*speed;
		inDirection   = physicsBall.velocity;
	}

	void Start() {
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}
}
