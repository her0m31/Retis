using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour {
	private Rigidbody2D physicsBall;
	private Vector2 inDirection;
	private GameObject outEffect;
	private GameObject hitEffect;
	private Vector2 worldPointMax;
	private Vector2 worldPointMin;

	private AudioSource hitSound;

	private Transform thisTransform;
	public new Transform transform {
		get {return thisTransform == null ? thisTransform = base.transform : thisTransform;}
	}

	private GameObject thisGameObject;
	public new GameObject gameObject {
		get {return thisGameObject == null ? thisGameObject = base.gameObject : thisGameObject;}
	}

	IEnumerator ResetTrail() {
		TrailRenderer trail = gameObject.GetComponent<TrailRenderer>();
		var trailTime = trail.time;
		trail.time = 0;
		yield return 0;
		trail.time = trailTime;
	}

	private void Reset() {
		this.StartCoroutine(ResetTrail());

		physicsBall.velocity = Vector2.zero;
		transform.position = Vector2.zero;

		physicsBall.velocity = AddFirstForce();
		inDirection = physicsBall.velocity;
	}

	private Vector2 AddFirstForce() {
		Vector2[] firstForce = new Vector2[] {
			new Vector2(-1.50f, -1.40f)*2.25f,
			new Vector2(-1.50f,  1.40f)*2.25f,
			new Vector2( 1.50f, -1.40f)*2.25f,
			new Vector2( 1.50f,  1.40f)*2.25f,
			new Vector2(-1.70f, -1.20f)*2.25f,
			new Vector2(-1.70f, -1.20f)*2.25f,
			new Vector2(-1.70f,  1.20f)*2.25f,
			new Vector2(-1.70f,  1.20f)*2.25f,
			new Vector2( 1.40f, -1.50f)*2.25f,
			new Vector2( 1.40f, -1.50f)*2.25f,
			new Vector2( 1.40f,  1.50f)*2.25f,
			new Vector2( 1.40f,  1.50f)*2.25f,
		};

		return firstForce[Random.Range(0, firstForce.Length)];
	}

	void EffectActive(Vector3 position, bool isHit) {
		if(isHit) {
			GameObject.Instantiate(hitEffect, position, Quaternion.identity);
			if(GameManager.IsPlay()) {
				hitSound.PlayOneShot(hitSound.clip);
			}
		}
		else {
			GameObject.Instantiate(outEffect, position, Quaternion.identity);
		}
	}

	void ChangeStateFromPlaying() {
		if(GameManager.State.Value == GameManager.GameState.Playing) {
			GameManager.State.Value = GameManager.GameState.GameOver;
		}
		else {
			GameManager.State.Value = GameManager.GameState.Restart;
		}
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
				this.enabled = false;
				break;
			case GameManager.GameState.Ready:
				this.enabled = false;
				Reset();
				break;
			case GameManager.GameState.Playing:
				this.enabled = true;
				break;
			case GameManager.GameState.GameOver:
				this.enabled = false;
				break;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// 衝突エフェクト発生
		if(coll.gameObject.CompareTag("OutOfArea") && GameManager.IsPlay()) {
			EffectActive(coll.contacts[0].point, false);
			GameManager.State.Value = GameManager.GameState.GameOver;
			Destroy(gameObject);
		}
		else {
			EffectActive(coll.contacts[0].point, true);
		}

		// 正規化された法線ベクトルを取得
		Vector2 inNormal = coll.contacts[0].normal;
		// vectorV => 入射ベクトル
		physicsBall.velocity = Vector2.Reflect(inDirection, inNormal);
		// 反射ベクトルを次の入射ベクトルとして保持
		inDirection = physicsBall.velocity;
	}

	void Update() {
		// ボールが完全に静止した時にリスタートする
		if(physicsBall.velocity == Vector2.zero) {
			EffectActive(transform.position, false);
			Destroy(gameObject);
			ChangeStateFromPlaying();
		}

		// ボールが画面外に出てしまった時にゲームオーバーとして終了させる
		// 現状だとボールスピードが変化しないのできにしなくて良い。
		if(transform.position.x < worldPointMin.x || worldPointMax.x < transform.position.x){
			if(transform.position.y < worldPointMin.y || worldPointMax.y < transform.position.y){
				Destroy(gameObject);
				ChangeStateFromPlaying();
			}
		}
	}

	void OnDestroy() {
		if(GameManager.Instance != null) {
			GameManager.State.RemoveListener(OnChangeGameState);
		}
	}

	void Start() {
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}

	void Awake() {
		outEffect = GameObject.Find("DeathEffect");
		hitEffect = GameObject.Find("SuccesEffects");

		worldPointMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		worldPointMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		physicsBall = gameObject.GetComponent<Rigidbody2D>();
		physicsBall.velocity = AddFirstForce();
		inDirection = physicsBall.velocity;

		hitSound = GetComponent<AudioSource>();
	}
}
