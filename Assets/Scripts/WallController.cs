using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {
	private Vector2 basePoint = new Vector2(0.0f, 0.0f);
	private Vector2 nextPoint = new Vector2(0.0f, 0.0f);

	private float speed = 1.0f;
	private Vector2 start;
	private Vector2 end;

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			start = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			nextPoint.x = 0.0f;
			nextPoint.y = 0.0f;
			basePoint.x = Input.mousePosition.x;
			basePoint.y = Input.mousePosition.y;
		}

		if(Input.GetMouseButton(0)) {
			end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			transform.position = Vector2.MoveTowards(transform.position,
			new Vector2(transform.position.x, transform.position.y + (start.y - end.y) / -30.0f),
			speed);

			nextPoint.x = basePoint.x - Input.mousePosition.x;
			nextPoint.y = basePoint.y - Input.mousePosition.y;
			basePoint.x = Input.mousePosition.x;
			basePoint.y = Input.mousePosition.y;

			// Vector3 aular = new Vector3(0.0f, 0.0f, nextPoint.y);
			//
			// transform.Rotate(aular, Space.World);
		}
	}
}
