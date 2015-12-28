using UnityEngine;
using System.Collections;

public class RacketsController : MonoBehaviour {
	private Transform thisTransform;
	private GameObject thisGameObject;
	private Vector2 worldPointMax;
	private Vector2 worldPointMin;
	private Vector2 clickStartPoint;
	private Vector2 clickEndPoint;
	private Vector2 targetPoint;

	public new Transform transform {
		get {
			return thisTransform == null ? thisTransform = base.transform : thisTransform;
		}
	}

	public new GameObject gameObject {
		get {
			return thisGameObject == null ? thisGameObject = base.gameObject : thisGameObject;
		}
	}

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			clickStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		if(Input.GetMouseButton(0)) {
			clickEndPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if(gameObject.name == "TBRackets") {
				targetPoint = new Vector2(transform.position.x + (clickStartPoint.x - clickEndPoint.x) / -20.0f,
					transform.position.y);
				targetPoint.x = Mathf.Clamp(targetPoint.x, worldPointMin.x, worldPointMax.x);
			}
			else {
				targetPoint = new Vector2(transform.position.x,
					transform.position.y + (clickEndPoint.y - clickStartPoint.y) / 20.0f);
				targetPoint.y = Mathf.Clamp(targetPoint.y, worldPointMin.y, worldPointMax.y);
			}

			transform.position = targetPoint;
		}
	}

	void Awake() {
		worldPointMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		worldPointMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
	}
}
