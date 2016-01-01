using UnityEngine;
using System.Collections;

public class RacketsController : MonoBehaviour {
	private Vector2 clickStartPoint;
	private Vector2 clickEndPoint;

	private Transform thisTransform;
	private GameObject thisGameObject;
	private Camera thisCamera;

	public new Transform transform {
		get {return thisTransform == null ? thisTransform = base.transform : thisTransform;}
	}

	public new GameObject gameObject {
		get {return thisGameObject == null ? thisGameObject = base.gameObject : thisGameObject;}
	}

	public new Camera camera {
		get {return thisCamera == null ? thisCamera = Camera.main : thisCamera;}
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Playing:
			this.enabled = true;
			break;
			case GameManager.GameState.Title:
			this.enabled = true;
			break;
			default:
			this.enabled = false;
			break;
		}
	}

	void OnDestroy() {
		if(GameManager.Instance != null) {
			GameManager.State.RemoveListener(OnChangeGameState);
		}
	}

	void Update() {
		// 画面をタッチ(右クリック)した時
		if(Input.GetMouseButtonDown(0)) {
			// タッチ(クリック)開始の座標を取得
			clickStartPoint = camera.ScreenToWorldPoint(Input.mousePosition);
		}
		// 画面をタッチ(右クリック)して、ドラッグしている状態
		if(Input.GetMouseButton(0)) {
			// タッチ(クリック)のドラッグの終了座標
			clickEndPoint = camera.ScreenToWorldPoint(Input.mousePosition);
			// 画面の右上の座標と左下の座標を取得
			Vector2 worldPointMax = camera.ViewportToWorldPoint(Vector2.one);
			Vector2 worldPointMin = camera.ViewportToWorldPoint(Vector2.zero);

			// オブジェクトの座標に、(クリックの終点から開始点を引いた値 / 20.0f)をプラス。
			// 20.0fは、ラケットの移動速度を調整するため.値を大きくすると遅く。小さくすると早くなる。
			if(gameObject.name == "TBRackets") {
				float x = transform.position.x + (clickEndPoint.x - clickStartPoint.x) / 15.0f;
				x = Mathf.Clamp(x, worldPointMin.x, worldPointMax.x);
				transform.position = new Vector2(x, transform.position.y);
			}
			else {
				float y = transform.position.y + (clickEndPoint.y - clickStartPoint.y) / 10.0f;
				y = Mathf.Clamp(y, worldPointMin.y, worldPointMax.y);
				transform.position = new Vector2(transform.position.x, y);
			}
		}
	}

	void Start() {
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}
}
