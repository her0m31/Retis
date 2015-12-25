using UnityEngine;
using System.Collections;

public class RacketsRotateController : MonoBehaviour {
	private Vector2 worldPointMax;
	private Vector2 worldPointMin;
	private Vector2 clickStartPoint;
	private Vector2 clickEndPoint;
	private Vector2 targetPoint;

	//Rotate Test
	private Vector2 basePoint = new Vector2(0.0f, 0.0f);
	private Vector2 nextPoint = new Vector2(0.0f, 0.0f);


	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			clickStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//RotateTest
			nextPoint.x = 0.0f;
			nextPoint.y = 0.0f;
			basePoint.x = Input.mousePosition.x;
			basePoint.y = Input.mousePosition.y;
		}
		if(Input.GetMouseButton(0)) {
			clickEndPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);


			if(gameObject.name == "Top" || gameObject.name == "Bottom") {
				targetPoint = new Vector2(transform.position.x
				+ (clickStartPoint.x - clickEndPoint.x) / -30.0f, transform.position.y);
			}
			else {
				targetPoint = new Vector2(transform.position.x,
				transform.position.y + (clickStartPoint.y - clickEndPoint.y) / -30.0f);
			}

			targetPoint.x = Mathf.Clamp(targetPoint.x, worldPointMin.x, worldPointMax.x);
			targetPoint.y = Mathf.Clamp(targetPoint.y, worldPointMin.y, worldPointMax.y);

			transform.position = targetPoint;

			//RotateTest
			nextPoint.x = basePoint.x - Input.mousePosition.x;
			nextPoint.y = basePoint.y - Input.mousePosition.y;
			basePoint.x = Input.mousePosition.x;
			basePoint.y = Input.mousePosition.y;
			Vector3 aular = new Vector3(0.0f, 0.0f, nextPoint.y);
			transform.Rotate(aular, Space.World);
		}
	}

	void Awake() {
		worldPointMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		worldPointMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
	}
}
