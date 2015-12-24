using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {
	private Vector2 worldPointMax;
	private Vector2 worldPointMin;
	private Vector2 clickStartPoint;
	private Vector2 clickEndPoint;
	private float speed = 1.0f;

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			clickStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		if(Input.GetMouseButton(0)) {
			clickEndPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector2 target = new Vector2(transform.position.x,
			transform.position.y + (clickStartPoint.y - clickEndPoint.y) / -30.0f);

			target.x = Mathf.Clamp(target.x, worldPointMin.x, worldPointMax.x);
			target.y = Mathf.Clamp(target.y, worldPointMin.y, worldPointMax.y);

			transform.position = target;
		}
	}

	void Awake() {
		worldPointMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		worldPointMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
	}
}
